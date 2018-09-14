using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvcFramework.Contracts.Generics
{
    public interface IActionResult<TModel> : IInvocable
    {
        IRenderable<TModel> Action { get; set; }

    }
}
