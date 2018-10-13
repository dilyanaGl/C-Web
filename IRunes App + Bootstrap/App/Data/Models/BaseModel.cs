using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data.Models
{
    public abstract class BaseModel<TKeyIdentifier>
    {
        public TKeyIdentifier Id { get; set; }
    }
}
