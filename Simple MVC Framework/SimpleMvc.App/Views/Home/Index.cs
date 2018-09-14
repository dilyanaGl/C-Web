using System;
using System.Collections.Generic;
using System.Text;
using SimpleMvcFramework.Contracts;

namespace SimpleMvc.App.Views.Home
{
   public class Index : IRenderable
    {
        public string Render()
        {
            return "<h3>Hello MVC!</h3>";

        }
    }
}
