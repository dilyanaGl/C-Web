using System;
using SimpleMvc.Framework.Contracts.Generics;

namespace SimpleMvc.Framework.ViewEngine.Generic
{
    public class ActionResult<T> : IActionResult<T>
    {
        public ActionResult(string viewFullQlifiedName, T model)
        {
            this.Action = (IRenderable<T>)
                Activator
                    .CreateInstance(
                        Type.GetType(viewFullQlifiedName));

            this.Action.Model = model;
        }

        public string Invoke()
        {
            return this.Action.Render();
        }
        
        public IRenderable<T> Action { get; set; }
      
    }
}
