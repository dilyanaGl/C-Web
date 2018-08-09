using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using HTTPServer.GameWorkshop.Data;
using HTTPServer.GameWorkshop.Data.Models;
using HTTPServer.GameWorkshop.Services.Contracts;
using HTTPServer.GameWorkshop.ViewModels.Shopping;

namespace HTTPServer.GameWorkshop.Services
{
    public class ShoppingService : IShoppingService
    {
        public bool AddToCart(string email, int gameId)
        {
            using (var db = new GameDbContext())
            {
                if (db.UserGames.Any(p => p.User.Email == email && p.GameId == gameId))
                {
                    return false;
                }

                var userGame = new UserGame
                {
                    Game = db.Games.SingleOrDefault(p => p.Id == gameId),
                    User = db.Users.SingleOrDefault(p => p.Email == email)
                };

                db.Add(userGame);
                db.SaveChanges();
            }

            return true;
        }

        public IEnumerable<ShoppingViewModel> ListItems(string email)
        {
            using (var db = new GameDbContext())
            {
               var games = db.UserGames
                    .Where(p => p.User.Email == email)
                    .Select(p => new ShoppingViewModel()
                    {
                        Id = p.GameId,
                        Title = p.Game.Title,
                        Description = p.Game.Description,
                        ImageThumbnail = p.Game.ImageThumbnail,
                        Price = p.Game.Price
                    })
                    .ToArray();

                return games;
            }
        }

        public void Remove(string email, int id)
        {
            using (var db = new GameDbContext())
            {
                var userGame = db.UserGames.SingleOrDefault(p => p.User.Email == email && p.Game.Id == id);

                db.UserGames.Remove(userGame);

                db.SaveChanges();
            }
        }

        public void Clear(string email)
        {
            using (var db = new GameDbContext())
            {
                var orders = db.UserGames.Where(p => p.User.Email == email);

                db.UserGames.RemoveRange(orders);

                db.SaveChanges();

            }
        }
    }

}
