using System;
using System.Collections.Generic;
using System.Text;
using SimpleMvcFramework.Contracts.Generics;

namespace SimpleMvcFramework.ViewEngine.Generic
{
    public class ActionResult<TModel> : IActionResult<TModel>
    {
        public ActionResult(string fullQualifiedName, TModel model)
        {
            this.Action = Activator.CreateInstance(Type.GetType(fullQualifiedName))
                as IRenderable<TModel>;

            if (this.Action == null)
            {
                throw new InvalidOperationException(
                    "The given view does not implement IRenderable<TModel>.");
            }

            this.Action.Model = model;
        }


        public string Invoke() => this.Action.Render();
     
        public IRenderable<TModel> Action { get; set; }
    }
}
