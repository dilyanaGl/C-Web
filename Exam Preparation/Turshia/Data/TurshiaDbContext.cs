using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Turshia.Data
{
    using Models;

    public class TurshiaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskParticipant> TaskParticipants { get; set; }
        public DbSet<TaskSector> TaskSectors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-V59G4I2\SQLEXPRESS;Database=TurshiaDb;Integrated Security=True;");
            }
        }

    }
}
