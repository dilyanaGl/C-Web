using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HTTPServer.Server.Contracts;

namespace HTTPServer.Application.Views
{
    public class FileView : IView
    {
        private string html;

        public FileView(string html)
        {
            this.html = html;
        }

        public string View()
        {
            return html;
        }
    }
}
