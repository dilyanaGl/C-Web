﻿using HTTPServer.Server.Contracts;

namespace HTTPServer.GameWorkshop.Views
{
    public class FileView : IView
    {
        private readonly string htmlFile;

        public FileView(string htmlFile)
        {
            this.htmlFile = htmlFile;
        }

        public string View() => this.htmlFile;
    }
}
