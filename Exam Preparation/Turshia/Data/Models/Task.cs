using System;
using System.Collections.Generic;
using System.Text;

namespace Turshia.Data.Models
{
    public class Task
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsReported { get; set; }

        public int Level { get; set; }

        public string Description { get; set; }

        public ICollection<TaskParticipant> Participants { get; set; } = new List<TaskParticipant>();

        public ICollection<TaskSector> AffectedSectors { get; set; } = new List<TaskSector>();

        public ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}
