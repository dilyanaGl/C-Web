using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer.Security
{
   public class User
   {
       private string username;
       private string password;

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
