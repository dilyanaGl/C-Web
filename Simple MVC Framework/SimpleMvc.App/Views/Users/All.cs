using System;
using System.Collections.Generic;
using System.Text;
using SimpleMvcFramework.Contracts;
using SimpleMvcFramework.Contracts.Generics;

namespace SimpleMvc.App.Views.Users
{
    using Models;

    public class All : IRenderable<AllUsernamesViewModel>
    {
       
        public AllUsernamesViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<h3>All users</h3>");
            sb.AppendLine("<ul>");
            foreach (var username in Model.Usernames)
            {
                sb.AppendLine($"<li><a href =\"/users/profile?id={username.Id}\">{username.Username}</a></li>");

            }
            sb.AppendLine("</ul>");
            return sb.ToString();

        }
    }
}
