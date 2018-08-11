using System;
using Microsoft.EntityFrameworkCore;
using SimpleMvc.Domain;

namespace SimpleMvc.Data
{
    public class NoteContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer("Server=.;DataBase=NoteDb;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Note>()
                .HasKey(p => p.Id);

            builder.Entity<User>()
                .HasKey(p => p.Id);

            builder.Entity<Note>()
                .HasOne(p => p.Owner)
                .WithMany(p => p.Notes)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
