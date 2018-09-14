using System;
using System.Collections.Generic;
using System.Text;
using SimpleMvcFramework.Contracts;

namespace SimpleMvc.App.Views.Users
{
    public class Register : IRenderable
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<h3>Register new user</h3>");
            sb.AppendLine("<form action=\"/users/register\" method=\"POST\"><br/>");
            sb.AppendLine("Username: <input type =\"text\" name=\"Username\"/><br/>");
            sb.AppendLine("Password: <input type= \"password\" name = \"Password\"/><br/>");
            sb.AppendLine("<input type= \"submit\" value = \"Register\"/><br/>");
            sb.AppendLine("</form><br/>");
            return sb.ToString();
        }
    }
}
