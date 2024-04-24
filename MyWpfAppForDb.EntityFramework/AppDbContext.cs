using Microsoft.EntityFrameworkCore;
using MyWpfAppForDb.EntityFramework.EntitiesBuilders;
using MyWpfAppForDb.EntityFramework.Entities;

namespace MyWpfAppForDb.EntityFramework
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext() {}

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
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
            if (!optionsBuilder.IsConfigured) {}
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
