using Microsoft.EntityFrameworkCore;
using System;
using MyWpfAppForDb.EntityFramework.Entities;

namespace MyWpfAppForDb.EntityFramework.EntitiesBuilders
{
	internal static class OrderBuilder
	{
		public static void OrderBuild(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Order>(entity =>
			{
				entity.Property(e => e.Id)
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
					.HasConstraintName("FK__Orders__client_i__6754599E")
					.OnDelete(DeleteBehavior.SetNull);

				entity.HasOne(d => d.DeliveryPoint)
					.WithMany(p => p.Orders)
					.HasForeignKey(d => d.DeliveryPointId)
					.HasConstraintName("FK__Orders__delivery__70DDC3D8")
					.OnDelete(DeleteBehavior.SetNull);
			});

			modelBuilder.Entity<Order>().HasData(
				new Order { Id = 1, DeliveryPointId = 1, ClientId = 1, OrderDate = new DateTime(2024, 01, 15, 08, 30, 00), Status = "Pending", TotalAmount = 704.49m },
				new Order { Id = 2, DeliveryPointId = 3, ClientId = 3, OrderDate = new DateTime(2024, 01, 16, 12, 45, 00), Status = "Shipped", TotalAmount = 325.23m },
				new Order { Id = 3, DeliveryPointId = 5, ClientId = 5, OrderDate = new DateTime(2024, 01, 17, 16, 20, 00), Status = "Delivered", TotalAmount = 226.00m },
				new Order { Id = 4, DeliveryPointId = 4, ClientId = 10, OrderDate = new DateTime(2024, 01, 18, 09, 10, 00), Status = "Pending", TotalAmount = 28.99m },
				new Order { Id = 5, DeliveryPointId = 5, ClientId = 5, OrderDate = new DateTime(2024, 01, 19, 11, 55, 00), Status = "Shipped", TotalAmount = 116.75m }
			);
		}
	}
}
