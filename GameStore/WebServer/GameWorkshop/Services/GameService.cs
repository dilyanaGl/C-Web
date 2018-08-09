using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTTPServer.GameWorkshop.Data;
using HTTPServer.GameWorkshop.Data.Models;
using HTTPServer.GameWorkshop.Services.Contracts;
using HTTPServer.GameWorkshop.ViewModels.Admin;

namespace HTTPServer.GameWorkshop.Services
{
    public class GameService : IGameService
    {
        public bool AddGame(string name, string image, double size, decimal price, string url, string description,
            DateTime releaseDate)
        {
            using (var db = new GameDbContext())
            {
                if (db.Games.Any(p => p.Title == name))
                {
                    return false;
                }

                var game = new Game
                {
                    Title = name, 
                    Description = description,
                    ImageThumbnail = image,
                    YouTubeVideoId = url,
                    Size = size,
                    Price = price,
                    ReleaseDate = releaseDate
                };

                db.Add(game);
                db.SaveChanges();
            }

            return true;
        }

        public IEnumerable<ListGamesViewModel> List()
        {
            using (var db = new GameDbContext())
            {
                return db.Games.Select(p => new ListGamesViewModel
                    {
                        Id = p.Id,
                        Name = p.Title,
                        Price = p.Price, 
                        Size = p.Size                                              
                     })
                    .ToArray();
            }
        }

        public bool EditGame(int id, string modelTitle, string modelImageThumbnail, double modelSize, decimal modelPrice,
            string modelYouTubeVideoId, string modelDescription, DateTime modelReleaseDate)
        {
            using (var db = new GameDbContext())
            {
                if (!db.Games.Any(p => p.Id == id))
                {
                    return false;
                }
                var game = db.Games.SingleOrDefault(p => p.Id == id);

                game.Title = modelTitle;
                game.ImageThumbnail = modelImageThumbnail;
                game.Size = modelSize;
                game.Price = modelPrice;
                game.Description = modelDescription;
                game.YouTubeVideoId = modelYouTubeVideoId;
                game.ReleaseDate = modelReleaseDate;

                db.SaveChanges();

            }

            return true;
        }

        public EditGamesViewModel FetchGame(int id)
        {
            using (var db = new GameDbContext())
            {
                var game = db.Games.Where(p => p.Id == id)
                    .Select(p => new EditGamesViewModel()
                    {
                        Id = id,
                        Title = p.Title,
                        Description = p.Description,
                        ImageThumbnail = p.ImageThumbnail,
                        YouTubeVideoId = p.YouTubeVideoId,
                        Size = p.Size,
                        Price = p.Price,
                        ReleaseDate = p.ReleaseDate
                    })
                    .SingleOrDefault();

                return game;
            }
        }

        public IEnumerable<HomeViewModel> DisplayHome()
        {
            using (var db = new GameDbContext())
            {
                var games = db.Games.Select(p => new HomeViewModel
                    {
                        Id = p.Id,
                        Title = p.Title, 
                        Description = p.Description,
                        Price = p.Price, 
                        Size = p.Size,
                        ImageThumbnail = p.ImageThumbnail
                    })
                    .ToArray();

                return games;
            }
        }

        public bool DeleteGame(int id)
        {
            using (var db = new GameDbContext())
            {
                if (!db.Games.Any(p => p.Id == id))
                {
                    return false;
                }

                var game = db.Games.SingleOrDefault(p => p.Id == id);
                db.Games.Remove(game);
                db.SaveChanges();
            }

            return true;
        }

        public GameDetailsViewModel GetDetails(int id)
        {
            using (var db = new GameDbContext())
            {
                var game = db.Games.Where(p => p.Id == id)
                    .Select(p => new GameDetailsViewModel()
                    {
                        Title = p.Title,
                        Description = p.Description,
                        Size = p.Size,
                        Price = p.Price, 
                        ReleaseDate = p.ReleaseDate,
                        YouTubeViewId = p.YouTubeVideoId


                    })
                .SingleOrDefault();

                return game;
            }
        }
    }
}

