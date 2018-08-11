using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvc.Framework.Contracts.Generics
{
    public interface IActionResult<T> : IInvocable
    {
        IRenderable<T> Action { get; set; }
    }
}
