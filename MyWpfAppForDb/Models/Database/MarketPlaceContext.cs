using System;
using System.Collections.Generic;
using MyWpfAppForDb.Models.Database.EachEntityBuilder;
using MyWpfAppForDb.Models.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyWpfAppForDb.Models.Database
{
    public partial class MarketPlaceContext : DbContext
    {
        public MarketPlaceContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public MarketPlaceContext(DbContextOptions<MarketPlaceContext> options) : base(options) 
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<DeliveryPoint> DeliveryPoints { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Market> Markets { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrdersItem> OrdersItems { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductsInstance> ProductsInstances { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=.;Database=MarketPlace;Trusted_Connection=True;TrustServerCertificate=true;");

                optionsBuilder.UseSqlite("Data Source=app.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.CategoryBuild();

            ClientBuilder.ClientBuild(modelBuilder);

            DeliveryPointBuilder.DeliveryPointBuild(modelBuilder);

            EmployeeBuilder.EmployeeBuild(modelBuilder);

            MarketBuilder.MarketBuild(modelBuilder);

            OrderBuilder.OrderBuild(modelBuilder);

            OrdersItemBuilder.OrdersItemBuild(modelBuilder);

            ProductBuilder.ProductBuild(modelBuilder);

            ProductsInstanceBuilder.ProductsInstanceBuild(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
