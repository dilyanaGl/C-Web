using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvc.Framework.Contracts
{
    public interface IActionResult : IInvocable
    {
        IRenderable Action { get; set; }
    }
}
