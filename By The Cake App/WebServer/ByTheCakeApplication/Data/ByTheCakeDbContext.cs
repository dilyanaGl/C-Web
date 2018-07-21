using System;
using System.Collections.Generic;
using System.Text;
using HTTPServer.ByTheCakeApplication.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HTTPServer.ByTheCakeApplication.Data
{
    public class ByTheCakeDbContext : DbContext
    {
        public ByTheCakeDbContext()
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=ByTheCakeDb;Integrated Security=True");

            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(p => p.Orders)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            builder.Entity<Product>()
                .HasMany(p => p.OrderProducts)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Order>()
                .HasMany(p => p.OrderProducts)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

           builder.Entity<OrderProduct>()
                .HasKey(p => new {p.OrderId, p.ProductId});


        }
    }
}
