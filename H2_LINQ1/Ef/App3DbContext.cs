using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace H2_LINQ1.Ef
{
    internal class App3DbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=H2_Catalogue;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .Property(i => i.Price)
                .HasConversion(toDb => toDb.Value, fromDb => new Price(fromDb));

            //Specifications gemmes som JSON i én kolonne i stedet for en separat tabel
            modelBuilder.Entity<Item>()
                .OwnsOne(i => i.Specifications, s => s.ToJson());
        }
    }
}
