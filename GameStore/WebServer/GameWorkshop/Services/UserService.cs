using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTTPServer.GameWorkshop.Data;
using HTTPServer.GameWorkshop.Data.Models;
using HTTPServer.GameWorkshop.Services.Contracts;

namespace HTTPServer.GameWorkshop.Services
{
    public class UserService : IUserService
    {
        public bool Create(string email, string name, string password)
        {
            using (var context = new GameDbContext())
            {
                if (context.Users.Any(p=> p.Email == email))
                {
                    return false;
                }

                var isAdmin = !context.Users.Any();

                var user = new User
                {
                    Email = email,
                    FullName = name,
                    Password = password,
                    IsAdmin = isAdmin
                };
                context.Add(user);
                context.SaveChanges();
                
            }

            return true;
        }

        public bool IsAdmin(string email)
        {
            using (var context = new GameDbContext())
            {
                return context.Users.Any(p => p.Email == email && p.IsAdmin);
            }
        }

        public bool Find(string email, string password)
        {
            using (var context = new GameDbContext())
            {
                return context.Users.Any(p => p.Email == email && p.Password == password);
            }
        }
    }
}
