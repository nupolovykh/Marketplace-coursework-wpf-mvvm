using Microsoft.EntityFrameworkCore;
using MyWpfAppForDb.EntityFramework.Entities;

namespace MyWpfAppForDb.EntityFramework.EntitiesBuilders
{

	internal static class CategoryBuilder
	{
		public static void CategoryBuild(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>(entity =>
			{
				entity.Property(e => e.Id)
					.ValueGeneratedNever()
					.HasColumnName("category_id");

				entity.Property(e => e.Name)
					.HasMaxLength(100)
					.HasColumnName("name");
			});

			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "Electronic" },
				new Category { Id = 2, Name = "Clothing" },
				new Category { Id = 3, Name = "Books" },
				new Category { Id = 4, Name = "Toys & Games" },
				new Category { Id = 5, Name = "Home & Kitchen" }
			);
		}
	}
}
