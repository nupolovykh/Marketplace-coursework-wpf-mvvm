using Microsoft.EntityFrameworkCore;
using System;
using MyWpfAppForDb.EntityFramework.Entities;

namespace MyWpfAppForDb.EntityFramework.EntitiesBuilders
{
	internal static class ProductBuilder
	{
		public static void ProductBuild(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>(entity =>
			{
				entity.Property(e => e.Id)
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

			modelBuilder.Entity<Product>().HasData(
				new Product { Id = 1, ProductInstanceId = 1, MarketId = 1, Price = 101.50m, Rating = 1.23m },
				new Product { Id = 2, ProductInstanceId = 2, MarketId = 2, Price = 15.75m, Rating = 1.78m },
				new Product { Id = 3, ProductInstanceId = 3, MarketId = 3, Price = 20.25m, Rating = 3.65m },
				new Product { Id = 4, ProductInstanceId = 4, MarketId = 4, Price = 182.99m, Rating = 3.44m },
				new Product { Id = 5, ProductInstanceId = 5, MarketId = 5, Price = 220.50m, Rating = 4.50m },
				new Product { Id = 6, ProductInstanceId = 1, MarketId = 1, Price = 30.00m, Rating = 5.00m },
				new Product { Id = 7, ProductInstanceId = 2, MarketId = 2, Price = 122.25m, Rating = 3.42m },
				new Product { Id = 8, ProductInstanceId = 3, MarketId = 3, Price = 170.99m, Rating = 3.71m },
				new Product { Id = 9, ProductInstanceId = 4, MarketId = 4, Price = 25.50m, Rating = 2.15m },
				new Product { Id = 10, ProductInstanceId = 5, MarketId = 5, Price = 198.75m, Rating = 3.89m },
				new Product { Id = 11, ProductInstanceId = 1, MarketId = 1, Price = 28.99m, Rating = 2.24m },
				new Product { Id = 12, ProductInstanceId = 2, MarketId = 2, Price = 32.25m, Rating = 3.65m },
				new Product { Id = 13, ProductInstanceId = 3, MarketId = 3, Price = 16.50m, Rating = 3.66m },
				new Product { Id = 14, ProductInstanceId = 4, MarketId = 4, Price = 23.75m, Rating = 2.95m },
				new Product { Id = 15, ProductInstanceId = 5, MarketId = 5, Price = 21.25m, Rating = 0.00m },
				new Product { Id = 16, ProductInstanceId = 1, MarketId = 1, Price = 147.99m, Rating = 3.32m },
				new Product { Id = 17, ProductInstanceId = 2, MarketId = 2, Price = 39.50m, Rating = 2.43m },
				new Product { Id = 18, ProductInstanceId = 3, MarketId = 3, Price = 27.25m, Rating = 4.78m },
				new Product { Id = 19, ProductInstanceId = 4, MarketId = 4, Price = 31.99m, Rating = 4.65m },
				new Product { Id = 20, ProductInstanceId = 5, MarketId = 5, Price = 183.75m, Rating = 2.29m }
			);
		}
	}
}
