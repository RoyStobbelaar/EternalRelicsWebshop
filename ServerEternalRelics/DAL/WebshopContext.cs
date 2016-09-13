using ServerEternalRelics.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ServerEternalRelics.DAL
{
    public class WebshopContext : DbContext
    {
        public WebshopContext()
            : base("WebshopContext")
        {

        }

        /* Create tables */
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderLine> OrderLine { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Offer> Offer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            /* fix Many to Many relation */
            modelBuilder.Entity<Category>()
                .HasMany<Product>(c => c.Products)
                .WithMany(p => p.Categories)
                .Map(pc =>
                {
                    pc.ToTable("ProductCategory");
                    pc.MapLeftKey("CategoryID");
                    pc.MapRightKey("ProductID");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}