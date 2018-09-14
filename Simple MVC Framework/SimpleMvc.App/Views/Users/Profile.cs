using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvc.App.Views.Users
{
    using SimpleMvcFramework.Contracts.Generics;
    using Models;

    public class Profile : IRenderable<UserProfileModel>
    {
        public UserProfileModel Model { get ; set ; }

        public string Render()
        {
            var sb = new StringBuilder();
            sb.AppendLine(@"<a href=""/users/all"">Back to All Users</a>");
            sb.AppendLine($"<h2>User: {Model.Username}</h2>");
            sb.AppendLine($"<form action = \"/users/profile?id={Model.Id}\" method = \"POST\">");
            sb.AppendLine("Title: <input type =\"text\" name=\"Title\"/>");
            sb.AppendLine("Content: <input type=\"text\" name=\"Content\" /> <br/>");
            sb.AppendLine("<input type=\"submit\" value=\"Add Note\"/>");
            sb.AppendLine("</form>");
            sb.AppendLine("<h5>List of notes</h5>");
            sb.AppendLine("<ul>");
            foreach (var note in Model.Notes)
            {
                sb.AppendLine($"<li><strong>{note.Title}</strong> - {note.Content}</li>");

            }

            sb.AppendLine("</ul>");

            return sb.ToString();
        }
    }
}
