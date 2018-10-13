using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace App.Services
{
    using Data;
    using Data.Models;

    public class UserService
    {
        private readonly IRunesDbContext context;
        private readonly IHashService hashService;

        public UserService()
        {
            this.context = new IRunesDbContext();
            this.hashService = new HashService();

        }

        public void RegisterUser(string username, string password, string email)
        {

            var hashedPassword = this.hashService.Hash(password);

            var user = new User
            {
                Name = username,
                HashedPassword = hashedPassword,
                Email = email

            };

            this.context.Users.Add(user);
            this.context.SaveChanges();

        }

        public bool UserExists(string username, string password)
        {
            string hashedPassword = this.hashService.Hash(password);
            return context.Users.Any(p => (p.Name == username || p.Email == username) && p.HashedPassword == hashedPassword);

        }
    }
}
