using System;
using System.Collections.Generic;
using System.Text;
using SimpleMvcFramework.Contracts;

namespace SimpleMvcFramework.ViewEngine
{
    public class ActionResult : IActionResult
    {
        public ActionResult(string fullQualifiedName)
        {
            this.Action = (IRenderable)Activator.CreateInstance(Type.GetType(fullQualifiedName));
        }

        public string Invoke() => this.Action.Render();
     

        public IRenderable Action { get; set; }
    }
}
