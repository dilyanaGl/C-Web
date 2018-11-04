using System;
using System.Collections.Generic;
using System.Text;

namespace Turshia.Data.Models
{
    public class TaskParticipant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TaskId { get; set; }

        public Task Task { get; set; }
    }
}
