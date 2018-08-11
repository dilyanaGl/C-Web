using System.IO;
using System.Text;
using SimpleMvc.Framework.Contracts;


namespace SimpleMvc.App.Views.Home
{
    public class Index : IRenderable
    {
        public string Render()
        {
            var sb = new StringBuilder();

            sb.AppendLine(File.ReadAllText(@"ViewFile\HtmlStart.html"));
            sb.AppendLine(@"<a href=""/users/all"">View All Users</a>");
            sb.AppendLine("<h3>Hello MVC!</h3>");
            sb.AppendLine(File.ReadAllText(@"ViewFile\HtmlEnd.html"));

            return sb.ToString();
        }
    }
}
