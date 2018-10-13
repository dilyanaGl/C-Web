using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvcFramework.Contracts
{
    public interface IViewable : IActionResult
    {
        IRenderable View { get; set; }
    }
}
