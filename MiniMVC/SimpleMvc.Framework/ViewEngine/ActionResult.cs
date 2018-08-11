using System;
using System.Collections.Generic;
using System.Text;
using SimpleMvc.Framework.Contracts;

namespace SimpleMvc.Framework.ViewEngine
{
    public class ActionResult : IActionResult
    {
        public ActionResult(string viewFullQualifiedName)
        {
            var type = Type.GetType(viewFullQualifiedName);
            Action = (IRenderable)Activator.CreateInstance(type);
        }

        public IRenderable Action { get; set; }

        public string Invoke()
        {
            return this.Action.Render();
        }


    }
}

