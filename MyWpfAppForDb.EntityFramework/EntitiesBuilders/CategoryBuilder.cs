using Microsoft.EntityFrameworkCore;
using MyWpfAppForDb.EntityFramework.Entities;

namespace MyWpfAppForDb.EntityFramework.EntitiesBuilders
{

    internal static class CategoryBuilder
    {
        public static void CategoryBuild(this ModelBuilder modelBuilder)
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

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Electronic" },
                new Category { CategoryId = 2, Name = "Clothing" },
                new Category { CategoryId = 3, Name = "Books" },
                new Category { CategoryId = 4, Name = "Toys & Games" },
                new Category { CategoryId = 5, Name = "Home & Kitchen" }
            );
        }
    }
}
