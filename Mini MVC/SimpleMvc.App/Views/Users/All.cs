using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SimpleMvc.App.ViewModels;
using SimpleMvc.Framework.Contracts.Generics;

namespace SimpleMvc.App.Views.Users
{
    public class All : IRenderable<AllUsernamesViewModel>
    {
        public AllUsernamesViewModel Model { get; set; }
        
        public string Render()
        {
            var sb = new StringBuilder();

            sb.AppendLine(File.ReadAllText(@"ViewFile\HtmlStart.html"));

            sb.AppendLine("<h3>All users</h3>");
            sb.AppendLine(@"<a href=""/home/index"">Back to Home</a>");
            sb.AppendLine("<ul>");
            foreach (var name in Model.Usernames)
            {
                sb.AppendLine($"<li><a href=\"/users/profile?id={name.Id}\">{name.Username}</a></li>");
            }
            sb.AppendLine("</ul>");
            sb.AppendLine(File.ReadAllText(@"ViewFile\HtmlEnd.html"));

            return sb.ToString();
        }
    }
}
