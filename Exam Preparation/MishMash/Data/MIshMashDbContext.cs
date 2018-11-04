using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Chushka.Data
{
    using Models;

    public class MishMashDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ChannelTag> ChannelTags { get; set; }
        public DbSet<UserChannel> UserChannels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-V59G4I2\SQLEXPRESS;Database=MishMash;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
