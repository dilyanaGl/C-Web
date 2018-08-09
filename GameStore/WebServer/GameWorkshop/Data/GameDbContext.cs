using System;
using System.Collections.Generic;
using System.Text;
using HTTPServer.GameWorkshop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HTTPServer.GameWorkshop.Data
{
    public class GameDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<UserGame> UserGames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasKey(p => p.Id);

            builder.Entity<User>()
                .HasIndex(p => p.Email)
                .IsUnique(true);

            builder.Entity<User>()
                .Property(p => p.Email)
                .IsUnicode(false);

            builder.Entity<User>()
                .Property(p => p.Password)
                .IsUnicode(false);

            builder.Entity<Game>()
                .HasKey(p => p.Id);

            builder.Entity<Game>()
                .Property(p => p.Title)
                .IsUnicode();

            builder.Entity<Game>()
                .HasIndex(p => p.Title)
                .IsUnique();

            builder.Entity<UserGame>()
                .HasKey(p => new
                {
                    p.UserId,
                    p.GameId
                });

            builder.Entity<UserGame>()
                .HasOne(p => p.User)
                .WithMany(p => p.Games)
                .HasForeignKey(p => p.UserId);

            builder.Entity<UserGame>()
                .HasOne(p => p.Game)
                .WithMany(p => p.Users)
                .HasForeignKey(p => p.GameId);


        }
    }
}
