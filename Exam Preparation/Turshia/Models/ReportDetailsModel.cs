using System;
using System.Collections.Generic;
using System.Text;

namespace Turshia.Models
{
   public class ReportDetailsModel
    {
        public string Id { get; set; }

        public string TaskName { get; set; }

        public string DueDate { get; set; }

        public int Level { get; set; }

        public string Status { get; set; }

        public string Participants { get; set; }

        public string Description { get; set; }

        public string AffectedSectors { get; set; }

        public string ReportedOn { get; set; }

        public string Reporter { get; set; }
    }
}
