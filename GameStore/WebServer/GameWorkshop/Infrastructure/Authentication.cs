using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer.GameWorkshop.Infrastructure
{
    public class Authentication
    {
        public Authentication(bool isAuthenticated, bool isAdmin)
        {
            IsAdmin = isAdmin;
            IsAuthenticated = isAuthenticated;
        }

        public bool IsAdmin { get; set;}

        public bool IsAuthenticated { get; set; }
    }
}
