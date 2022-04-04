using EXresturant.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EXresturant.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Reservering> Reserverings { get; set; }
        public DbSet<Menukaart> Menukaarts { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<BonBewaren> Bons { get; set; }
        public DbSet<Bestelling> Bestellings { get; set; }
        public DbSet<BestellingItem> BestellingItems { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Bestelling>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<BestellingItem>().Property(t => t.Id).ValueGeneratedOnAdd();

            //modelBuilder.Entity<Order>()
            //    .HasOne<Reservering>(e => e.Reservering)
            //    .WithMany(d => d.Orders)
            //    .HasForeignKey(e => e.ReserveringId)
            //    .IsRequired(true)
            //    .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Menukaart>().HasData(

                new Menukaart { Id = 1, Naam = "Lunchkaart" },
                new Menukaart { Id = 2, Naam = "Dinerkaart" },
                new Menukaart { Id = 3, Naam = "Drankenkaart" }

                );

            modelBuilder.Entity<Categorie>().HasData(

                new Categorie { Id = 1, MenukaartId = 1, Naam = "HoodGericht" },
                new Categorie { Id = 2, MenukaartId = 2, Naam = "NaGericht" },
                new Categorie { Id = 3, MenukaartId = 3, Naam = "Friesdrank" }

                );
            modelBuilder.Entity<Product>().HasData(

                new Product { Id = 1, CategorieId = 1, MenukaartId = 1,Price = 6, Name = "pizza" },
                new Product { Id = 2, CategorieId = 2, MenukaartId = 2, Price = 4, Name = "soep" },
                new Product { Id = 3, CategorieId = 3, MenukaartId = 3, Price = 2, Name = "pepsi" }

               );

        }

    }
}

