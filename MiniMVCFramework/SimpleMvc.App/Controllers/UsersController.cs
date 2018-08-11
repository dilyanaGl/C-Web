using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleMvc.App.BindingModels;
using SimpleMvc.App.ViewModels;
using SimpleMvc.Data;
using SimpleMvc.Domain;
using SimpleMvc.Framework.Attributes.Methods;
using SimpleMvc.Framework.Contracts;
using SimpleMvc.Framework.Contracts.Generics;
using SimpleMvc.Framework.Controllers;

namespace SimpleMvc.App.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult<AllUsernamesViewModel> Register(RegisterBindingModel model)
        {
            var user = new User()
            {
                Username = model.Username,
                Password = model.Password
            };

            using (var db = new NoteContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }

                return All();
        }

        [HttpGet]
        public IActionResult<AllUsernamesViewModel> All()
        {
            List<UserViewModel> usernames = new List<UserViewModel>();

            using (var context = new NoteContext())
            {
                usernames = context.Users
                    .Select(p => new UserViewModel()
                    {
                        Username = p.Username,
                        Id = p.Id
                    })
                    .ToList();
            }

            var model = new AllUsernamesViewModel();
            model.Usernames = usernames;

            return View(model);

        }

        [HttpGet]
        public IActionResult<UserProfileViewModel> Profile(int id)
        {
            using (var db = new NoteContext())
            {
                var user = db.Users.Where(p => p.Id == id)
                    .Select(p => new UserProfileViewModel()
                    {
                        UserId = p.Id,
                        Username = p.Username,
                        Notes = p.Notes.Select(k => new NoteViewModel()
                            {
                                Title = k.Title,
                                Content = k.Content
                            })
                            .ToArray()
                    })
                    .SingleOrDefault();

                return View(user);

            }
        }

        [HttpPost]
        public IActionResult<UserProfileViewModel> Profile(AddNoteBindingModel model)
        {
            using (var db = new NoteContext())
            {
                var user = db.Users.Find(model.UserId);
                var note = new Note
                {
                    Title = model.Title,
                    Content = model.Content,
                    Owner = user
                };
                db.Notes.Add(note);
                db.SaveChanges();
            }

            return Profile(model.UserId);
        }
    }
}
