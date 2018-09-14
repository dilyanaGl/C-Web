using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvcFramework.Contracts
{
    public interface IActionResult : IInvocable
    {
        IRenderable Action { get; set; }
    }
}
