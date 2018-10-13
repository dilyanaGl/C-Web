using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvcFramework.Contracts
{
   public interface IRedirectable : IActionResult
    {
        string RedirectUrl { get; }
    }
}
