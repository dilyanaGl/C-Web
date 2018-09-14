using System;
using HTTPServer.Application.Helpers;
using HTTPServer.Server.Http.Contracts;
using System.Collections.Generic;
using HTTPServer.Application.Models;
using System.IO;
using System.Linq;
using System.Text;
using HTTPServer.Security;

namespace HTTPServer.Application.Controllers
{
    public class CakeController : ViewReader
    {
        private static List<Cake> cakes = new List<Cake>();

        private const string filePath = "Data\\data.txt";
        private const string ordersFilePath = "Data\\orders.txt";

        public IHttpResponse Add()
        {
            if (!Session.IsUserLoggedIn())
            {
                return this.GetResponse("logIn");
            }

            return GetResponse("add", new Dictionary<string, string>()
            {
                ["display"] = "none"
            });
        }

        public IHttpResponse Add(string name, string price)
        {
             if (!Session.IsUserLoggedIn())
            {
                return this.GetResponse("logIn");
            }

            var cake = new Cake
            {
               Name = name,
               Price = Decimal.Parse(price)
            };

           cakes.Add(cake);

            using (var writer = new StreamWriter(filePath, true))
            {
               writer.WriteLine($"{name}, {price}");
            }

          
            return this.GetResponse("add", new Dictionary<string, string>()
            {
                ["Name"] = name,
                ["Price"] = price,
                ["display"] = "block"
            });

         }

        public IHttpResponse Search(IDictionary<string, string> urlParameters)
        {
            string display = "none";
            string cartDisplay = "none";
            if (!Session.IsUserLoggedIn())
            {
                return this.GetResponse("logIn");
            }
            var result = string.Empty;
            var key = "Name";

            
            if (urlParameters.ContainsKey(key))
            {
                
                var value = urlParameters[key];
                var allCakesAsDivs = File.ReadAllLines(filePath)
                    .Where(p => p.Contains(','))
                    .Select(p => p.Split(','))
                    .Select(l => new Cake()
                    {
                        Name = l[0].Trim(),
                        Price = decimal.Parse(l[1].Trim())
                    })
                    .Where(p => p.Name.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                    .Select(p => $"<div>{p.Name} - ${p.Price}" +
                                 $"<form method = \"POST\" action = \"/addToCart\">" +
                                 $"<input type =\"hidden\" name = \"Cake\" value = \"{p.Name} - {p.Price}\"/>" +
                                 $"<input type=\"submit\" value=\"Order\"/>" +
                                 $"</form>");

                display = "block";

                result = string.Join(Environment.NewLine, allCakesAsDivs);
            }

            return GetResponse("search", new Dictionary<string, string>()
            {
                ["cartDisplay"] = cartDisplay,
                ["display"] = display,
                ["results"] = result
            });
        }

        public IHttpResponse AddToCart(string itemName)
        {

            //using (var writer = new StreamWriter(ordersFilePath))
            //{
            //    writer.WriteLine(itemName);
            //}

            File.AppendAllLines(ordersFilePath, new List<string>(){itemName});

            var text = File.ReadAllLines(ordersFilePath);

            var sb = new List<string>();

            decimal totalPrice = 0;

            foreach (var line in text)
            {
                if (String.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var name = line.Split(' ').First();
                var price = decimal.Parse(line.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries).Last());
                totalPrice += price;

                sb.Add($"<p>{name} - ${price}</p>");

            }

            return GetResponse("shoppingCart", new Dictionary<string, string>()
            {
                ["results"] = String.Join(Environment.NewLine, sb),
                ["totalPrice"] = Convert.ToString(totalPrice)
            });

                
        }

        public IHttpResponse GetCart()
        {
            return GetResponse("shoppingCart");
        }


        public IHttpResponse FinishOrder()
        {
            return GetResponse("finishOrder");
        }
    }
}
