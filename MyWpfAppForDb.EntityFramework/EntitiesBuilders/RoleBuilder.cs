using Microsoft.EntityFrameworkCore;
using MyWpfAppForDb.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWpfAppForDb.EntityFramework.EntitiesBuilders
{
	internal static class RoleBuilder
	{
		public static void RoleBuild(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Role>(entity =>
			{
				entity.Property(e => e.Id)
					.ValueGeneratedNever()
					.HasColumnName("role_id");

				entity.Property(e => e.Name)
					.HasMaxLength(100)
					.HasColumnName("name");
			});

			modelBuilder.Entity<Role>().HasData(
				new Role { Id = 1, Name = "Admin" },
				new Role { Id = 2, Name = "Loader" },
				new Role { Id = 3, Name = "Operator" }
			);
		}
	}
}
