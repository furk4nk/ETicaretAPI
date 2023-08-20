using ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Context
{
    public class ETicaretAPIDbContext : DbContext
    {
        public DbSet<Order> orders { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Product> products { get; set; }
        public ETicaretAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(a =>
            {
                a.ToTable("Orders").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Address).HasColumnName("Address");
                a.Property(p => p.CreatedDate).HasColumnName("CreatedDate").HasColumnType("datetime2").HasPrecision(0);
                a.Property(p => p.CustomerId).HasColumnName("CustomerId");
                a.Property(p => p.Description).HasColumnName("Description");
                a.HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId);
                a.HasMany(x => x.Products)
                .WithMany(x => x.Orders);
            });

            modelBuilder.Entity<Customer>(a =>
            {
                a.ToTable("Customers").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.Property(p => p.CreatedDate).HasColumnName("CreatedDate").HasColumnType("datetime2").HasPrecision(0);
                a.HasMany(x => x.Orders)
                .WithOne(x => x.Customer);
            });

            modelBuilder.Entity<Product>(a =>
            {
                a.ToTable("Products").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.CreatedDate).HasColumnName("CreatedDate").HasColumnType("datetime2").HasPrecision(0);
                a.Property(p => p.Name).HasColumnName("Name");
                a.Property(p => p.Stock).HasColumnName("Stock");
                a.Property(p => p.Price).HasColumnName("Price").HasColumnType("real");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
