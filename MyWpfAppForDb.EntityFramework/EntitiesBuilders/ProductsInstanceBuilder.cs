using Microsoft.EntityFrameworkCore;
using MyWpfAppForDb.EntityFramework.Entities;

namespace MyWpfAppForDb.EntityFramework.EntitiesBuilders
{
	internal static class ProductsInstanceBuilder
	{
		public static void ProductsInstanceBuild(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductsInstance>(entity =>
			{
				entity.HasKey(e => e.Id)
					.HasName("PK__Products__E935EA68E11C4348");

				entity.ToTable("Products_Instances");

				entity.Property(e => e.Id)
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
					.HasConstraintName("FK__Products___categ__60A75C0F")
					.OnDelete(DeleteBehavior.SetNull);
			});

			modelBuilder.Entity<ProductsInstance>().HasData(
				new ProductsInstance { Id = 1, CategoryId = 1, Name = "Smart TV", Description = "Description for Smart TV", Availability = true },
				new ProductsInstance { Id = 2, CategoryId = 1, Name = "Smartphone", Description = "Description for Smartphone", Availability = true },
				new ProductsInstance { Id = 3, CategoryId = 2, Name = "T-shirts", Description = "Description for T-shirts", Availability = false },
				new ProductsInstance { Id = 4, CategoryId = 2, Name = "Jeans", Description = "Description for Jeans", Availability = true },
				new ProductsInstance { Id = 5, CategoryId = 3, Name = "Stainless Steel Cookware Set", Description = "Description for Stainless Steel Cookware Set", Availability = true },
				new ProductsInstance { Id = 6, CategoryId = 3, Name = "Kitchen Knife Set", Description = "Description for Kitchen Knife Set", Availability = false },
				new ProductsInstance { Id = 7, CategoryId = 4, Name = "\"To Kill a Mockingbird\" by Harper Lee", Description = "Description for \"To Kill a Mockingbird\" by Harper Lee", Availability = true },
				new ProductsInstance { Id = 8, CategoryId = 4, Name = "\"1984\" by George Orwell", Description = "Description for \"1984\" by George Orwell", Availability = true },
				new ProductsInstance { Id = 9, CategoryId = 5, Name = "LEGO Classic Creative Bricks Set", Description = "Description for LEGO Classic Creative Bricks Set", Availability = false },
				new ProductsInstance { Id = 10, CategoryId = 5, Name = "Monopoly Board Game", Description = "Description for Monopoly Board Game", Availability = true },
				new ProductsInstance { Id = 11, CategoryId = 1, Name = "Laptop", Description = "Description for Laptop", Availability = true },
				new ProductsInstance { Id = 12, CategoryId = 1, Name = "Wireless Headphones", Description = "Description for Wireless Headphones", Availability = true },
				new ProductsInstance { Id = 13, CategoryId = 2, Name = "Jackets", Description = "Description for Jackets", Availability = false },
				new ProductsInstance { Id = 14, CategoryId = 2, Name = "Shoes", Description = "Description for Shoes", Availability = true },
				new ProductsInstance { Id = 15, CategoryId = 3, Name = "Glass Food Storage Containers", Description = "Description for Glass Food Storage Containers", Availability = true },
				new ProductsInstance { Id = 16, CategoryId = 3, Name = "Coffee Maker", Description = "Description for Coffee Maker", Availability = false },
				new ProductsInstance { Id = 17, CategoryId = 4, Name = "\"The Alchemist\" by Paulo Coelho", Description = "Description for \"The Alchemist\" by Paulo Coelho", Availability = true },
				new ProductsInstance { Id = 18, CategoryId = 4, Name = "\"The Catcher in the Rye\" by J.D. Salinger", Description = "Description for \"The Catcher in the Rye\" by J.D. Salinger", Availability = true },
				new ProductsInstance { Id = 19, CategoryId = 5, Name = "Nerf N-Strike Elite Retaliator Blaster", Description = "Description for Nerf N-Strike Elite Retaliator Blaster", Availability = false },
				new ProductsInstance { Id = 20, CategoryId = 5, Name = "Rubiks Cube", Description = "Description for Rubiks Cube", Availability = true }
			);
		}
	}
}
