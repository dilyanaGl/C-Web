using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SimpleMvc.App.ViewModels;
using SimpleMvc.Framework.Contracts.Generics;

namespace SimpleMvc.App.Views.Users
{
    public class Profile : IRenderable<UserProfileViewModel>
    {
        public string Render()
        {

            var sb = new StringBuilder();
            //sb.AppendLine(File.ReadAllText(@"ViewFile\HtmlStart.html"));
            //sb.AppendLine("<form action=\"profile\" method=\"POST\"/>");
            //sb.AppendLine("Title: <input type=\"text\" name=\"Title\"/><br/>");
            //sb.AppendLine("Content: <input type=\"text\" name=\"Content\"/> <br/>");
            //sb.AppendLine($"<input type=\"hidden\" name=\"UserId\" value=\"{Model.UserId}\"");
            //sb.AppendLine(@"<input type=""submit"" value=Submit/> <br />");
            //sb.AppendLine("</form>");

            sb.AppendLine(File.ReadAllText(@"ViewFile\HtmlStart.html"));
            sb.AppendLine(@"<a href=""/users/all"">Back to All Users</a>");
            sb.AppendLine($"<h3>User: {Model.Username}</h3>");
            sb.AppendLine(@"<form action=""profile"" method=""post""");

            sb.AppendLine(@"<label for=""Title"">Title:</label>");
            sb.AppendLine(@"<input type=""text"" id=""Title"" name=""Title"" placeholder=""Enter Title"" required> <br />");
            sb.AppendLine(@"<label for=""Content"">Content:</label>");
            sb.AppendLine(@"<input type=""text"" id=""Content"" name=""Content"" placeholder=""Enter Content"" required> <br />");
            sb.AppendLine($@"<input type=""hidden"" name=""UserId"" value=""{Model.UserId}"">");

            sb.AppendLine(@"<button type=""submit"">Add note</button> <br />");
            sb.AppendLine(@"</form>");
            sb.AppendLine(@"<h5>List of notes</h5>");
            sb.AppendLine("<ul>");

            foreach (var note in Model.Notes)
            {
                sb.AppendLine($@"<li><strong>{note.Title}</strong> - {note.Content}</li>");
            }

            sb.AppendLine("</ul>");

            sb.AppendLine(File.ReadAllText(@"ViewFile\HtmlEnd.html"));
            return sb.ToString();
        }

        public UserProfileViewModel Model { get; set; }
    }
}
