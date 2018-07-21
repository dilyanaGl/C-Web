using System;
using System.Linq;
using HTTPServer.ByTheCakeApplication.Data;
using HTTPServer.ByTheCakeApplication.ViewModels.Account;

namespace HTTPServer.ByTheCakeApplication.Service.User
{
    public class UserService : IUserService
    {
        public bool Create(string username, string password)
        {

            using (var db = new ByTheCakeDbContext())
            {
                if (db.Users.Any(p => p.Username == username))
                {
                    return false;
                }
                var user = new Data.Models.User()
                {
                    Username = username,
                    Password = password,
                    RegistrationDate = DateTime.Now
                };


                db.Add(user);
                db.SaveChanges();

                return true;
            }
        }

        public bool Find(string username, string password)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Users.Any(p => p.Username == username && p.Password == password);
            }
        }

        public ProfileViewModel Profile(string username)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db
                    .Users
                    .Where(p => p.Username == username)
                    .Select(p => new ProfileViewModel()
                    {
                        Username = p.Username,
                        RegistrationDate = p.RegistrationDate,
                        TotalOrders = p.Orders.Count
                    })
                    .FirstOrDefault();

            }
        }

        public int? GetUserId(string username)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Users.FirstOrDefault(p => p.Username == username)?.Id;
            }
        }
    }
}
