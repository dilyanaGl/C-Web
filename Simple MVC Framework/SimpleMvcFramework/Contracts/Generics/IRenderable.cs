using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvcFramework.Contracts.Generics
{
    public interface IRenderable<TModel> : IRenderable
    {
        TModel Model { get; set; }
    }
}
