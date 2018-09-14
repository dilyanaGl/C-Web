using System;
using System.Collections.Generic;
using System.Text;
using SimpleMvcFramework.Attributes;
using SimpleMvcFramework.Contracts;
using SimpleMvcFramework.Contracts.Generics;
using SimpleMvcFramework.Controllers;
using System.Linq;


namespace SimpleMvc.App.Controllers
{
    using Data.Models;
    using Data;
    using Models;

    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return this.View();

        }

        [HttpPost]
        public IActionResult<AllUsernamesViewModel> Register(RegisterUserBindingModel registerUserBinding)
        {
            var user = new User
            {
                Username = registerUserBinding.Username,
                Password = registerUserBinding.Password
            };

            using (var context = new NoteDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();

            }

            return this.All();

        }

        [HttpGet]
        public IActionResult<AllUsernamesViewModel> All()
        {
            List<UsernameViewModel> usernames = new List<UsernameViewModel>();
            using (var context = new NoteDbContext())
            {

                var users = context.Users.Select(p => new UsernameViewModel
                {
                    Id = p.Id,
                    Username = p.Username
                })
                .ToList();

                usernames = users;

            }

            var model = new AllUsernamesViewModel
            {
                Usernames = usernames

            };


            return this.View(model);

        }

        [HttpGet]
        public IActionResult<UserProfileModel> Profile(int id)
        {
            using (var context = new NoteDbContext())
            {


                var user = context.Users.Where(p => p.Id == id)
                .Select(p => new UserProfileModel
                {
                    Id = p.Id,
                    Username = p.Username,
                    Notes = p.Notes.Select(k => new NoteViewModel()
                    {
                        Title = k.Title,
                        Content = k.Content

                    })
                })
                .SingleOrDefault();

                var viewModel = user;

                return this.View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult<UserProfileModel> Profile(int id, NoteViewModel model)
        {
            using (var db = new NoteDbContext())
            {
                var note = new Note
                {
                    Title = model.Title,
                    Content = model.Content,
                    UserId = id
                };

                db.Notes.Add(note);
                db.SaveChanges();
            }


                return this.Profile(id);
        }
    }
}
