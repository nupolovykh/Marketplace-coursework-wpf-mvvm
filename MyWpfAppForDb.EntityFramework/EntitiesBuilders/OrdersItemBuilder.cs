using Microsoft.EntityFrameworkCore;
using MyWpfAppForDb.EntityFramework.Entities;

namespace MyWpfAppForDb.EntityFramework.EntitiesBuilders
{
    internal static class OrdersItemBuilder
    {
        public static void OrdersItemBuild(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrdersItem>(entity =>
            {
                entity.ToTable("Orders_Items");

                entity.Property(e => e.Id)
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

            modelBuilder.Entity<OrdersItem>().HasData(
                new OrdersItem { Id = 1, OrderId = 1, ProductId = 1 },
                new OrdersItem { Id = 2, OrderId = 1, ProductId = 2 },
                new OrdersItem { Id = 3, OrderId = 1, ProductId = 20 },
                new OrdersItem { Id = 4, OrderId = 1, ProductId = 4 },
                new OrdersItem { Id = 5, OrderId = 1, ProductId = 5 },
                new OrdersItem { Id = 6, OrderId = 2, ProductId = 19 },
                new OrdersItem { Id = 7, OrderId = 2, ProductId = 7 },
                new OrdersItem { Id = 8, OrderId = 2, ProductId = 8 },
                new OrdersItem { Id = 9, OrderId = 3, ProductId = 18 },
                new OrdersItem { Id = 10, OrderId = 3, ProductId = 10 },
                new OrdersItem { Id = 11, OrderId = 4, ProductId = 11 },
                new OrdersItem { Id = 12, OrderId = 5, ProductId = 12 },
                new OrdersItem { Id = 13, OrderId = 5, ProductId = 17 },
                new OrdersItem { Id = 14, OrderId = 5, ProductId = 14 },
                new OrdersItem { Id = 15, OrderId = 5, ProductId = 15 }
            );
        }
    }
}
