using System.IO;
using System.Text;
using SimpleMvc.Framework.Contracts;

namespace SimpleMvc.App.Views.Users
{
    public class Register : IRenderable
    {
        public string Render()
        {
            //var sb = new StringBuilder();
            //sb.AppendLine("<h3>Register new user</h3>");
            //sb.AppendLine("<form action=\"/users/register\" method = \"POST\">");
            //sb.AppendLine("Username: <input type = \"text\" name = \"username\"/><br/>");
            //sb.AppendLine("Password: <input type = \"password\" name = \"password\"/> <br/>");
            //sb.AppendLine("<input type=\"submit\" value =\"Register\"/>");
            //sb.AppendLine("</form><br/>");
           
            //return sb.ToString();

            return File.ReadAllText(@"ViewFile\Register.html");
        }
    }
}
