using System;
using System.Collections.Generic;
using System.Text;

namespace Turshia.Models
{
    public class TaskDetailsModel
    {
        public string Name { get; set; }

        public int Level { get; set; }

        public string DueDate { get; set; }

        public string Participants { get; set; }

        public string AffectedSectors { get; set; }

        public string Description { get; set; }
    }
}
