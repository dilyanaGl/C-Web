using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SimpleMvcFramework.Views
{
    using Contracts;

    public class View : IRenderable
    {
         private readonly string fullyQualifiedTemplateName;

        public View(string fullyQualifiedTemplateName)
        {
            this.fullyQualifiedTemplateName = fullyQualifiedTemplateName;
        }

        public string Render()
        {
           var fullHtml = this.ReadFile(this.fullyQualifiedTemplateName);

            return fullHtml;
        }
       

        private string ReadFile(string fullyQualifiedTemplateName)
        {
            var path = $"../../../{fullyQualifiedTemplateName}";
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();

            }

            return File.ReadAllText(path);
        }
    }
}
