using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AppWPF.Database
{
    public partial class MarketPlaceContext : DbContext
    {
        public MarketPlaceContext()
        {
        }

        public MarketPlaceContext(DbContextOptions<MarketPlaceContext> options) : base(options)
        {}

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<DeliveryPoint> DeliveryPoints { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Market> Markets { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrdersItem> OrdersItems { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductsInstance> ProductsInstances { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=MarketPlace;Trusted_Connection=True;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("category_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.ClientId)
                    .ValueGeneratedNever()
                    .HasColumnName("client_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<DeliveryPoint>(entity =>
            {
                entity.ToTable("Delivery_Points");

                entity.Property(e => e.DeliveryPointId)
                    .ValueGeneratedNever()
                    .HasColumnName("delivery_point_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .HasColumnName("city");

                entity.Property(e => e.Rating)
                    .HasColumnType("decimal(3, 2)")
                    .HasColumnName("rating");

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(20)
                    .HasColumnName("zipcode");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("employee_id");

                entity.Property(e => e.DeliveryPointId).HasColumnName("delivery_point_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .HasColumnName("phone");

                entity.Property(e => e.Salary)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("salary");

                entity.HasOne(d => d.DeliveryPoint)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DeliveryPointId)
                    .HasConstraintName("FK__Employees__deliv__6FE99F9F");
            });

            modelBuilder.Entity<Market>(entity =>
            {
                entity.Property(e => e.MarketId)
                    .ValueGeneratedNever()
                    .HasColumnName("market_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .HasColumnName("city");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("order_id");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.DeliveryPointId).HasColumnName("delivery_point_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total_amount");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Orders__client_i__6754599E");

                entity.HasOne(d => d.DeliveryPoint)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DeliveryPointId)
                    .HasConstraintName("FK__Orders__delivery__70DDC3D8");
            });

            modelBuilder.Entity<OrdersItem>(entity =>
            {
                entity.ToTable("Orders_Items");

                entity.Property(e => e.OrdersItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("orders_item_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__Orders_It__order__66603565");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrdersItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Orders_It__produ__656C112C");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("product_id");

                entity.Property(e => e.MarketId).HasColumnName("market_id");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.ProductInstanceId).HasColumnName("product_instance_id");

                entity.Property(e => e.Rating)
                    .HasColumnType("decimal(3, 2)")
                    .HasColumnName("rating");

                entity.HasOne(d => d.Market)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.MarketId)
                    .HasConstraintName("FK__Products__market__6383C8BA");

                entity.HasOne(d => d.ProductInstance)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductInstanceId)
                    .HasConstraintName("FK__Products__produc__628FA481");
            });

            modelBuilder.Entity<ProductsInstance>(entity =>
            {
                entity.HasKey(e => e.ProductInstanceId)
                    .HasName("PK__Products__E935EA68E11C4348");

                entity.ToTable("Products_Instances");

                entity.Property(e => e.ProductInstanceId)
                    .ValueGeneratedNever()
                    .HasColumnName("product_instance_id");

                entity.Property(e => e.Availability).HasColumnName("availability");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductsInstances)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Products___categ__60A75C0F");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("user_id");

                entity.Property(e => e.Login)
                    .HasMaxLength(100)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasColumnName("role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
