using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Turshia.Services
{
    using Data;
    using Data.Models;
    using Models;
    using SIS.MvcFramework;

    public class UserService : IUserService
    {

        private readonly TurshiaDbContext context;

        private readonly IHashService hashService;

        public UserService(IHashService hashService)
        {

            this.context = new TurshiaDbContext();

            this.hashService = hashService;

        }



        public bool RegisterUser(RegisterViewModel model)
        {

            var hashedPassword = this.hashService.Hash(model.Password.Trim());

            if (this.context.Users.Any(p => p.Username == model.Username))
            {
                return false;
            }

            Role role = Role.User;

            if (!this.context.Users.Any())
            {
                role = Role.Admin;
            }

            var user = new User
            {

                Username = model.Username.Trim(),

                Password = hashedPassword,

                Email = model.Email.Trim(),

                Role = role


            };

            this.context.Users.Add(user);

            this.context.SaveChanges();

            return true;
        }

        public bool UserExists(LoginViewModel model)
        {

            string hashedPassword = this.hashService.Hash(model.Password);

            return context.Users.Any(p => (p.Username == model.Username || p.Email == model.Username) && p.Password == hashedPassword);
        }

        public MvcUserInfo GetUserInfo(string username)
        {
            return this.context.Users.Where(p => p.Username == username)
                .Select(p => new MvcUserInfo
                {
                    Username = p.Username,
                    Role = p.Role.ToString()
                })
                .SingleOrDefault();
        }


    }
}
