using System.Collections.Generic;

namespace HTTPServer.ByTheCakeApplication.Data
{
    public class CakesData
    {
        public ICollection<int> ProductIds { get; set; } = new List<int>();
    }
}