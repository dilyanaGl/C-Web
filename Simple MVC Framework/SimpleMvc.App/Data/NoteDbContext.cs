using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SimpleMvc.App.Data
{
    using Models;

    public class NoteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(@"Server=DESKTOP-V59G4I2\SQLEXPRESS;Database=NotesDb;Integrated Security=True;");
            }

        }    
    }
}
