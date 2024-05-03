using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using MyWpfAppForDb.EntityFramework.Entities;

namespace MyWpfAppForDb.EntityFramework.EntitiesBuilders
{
	internal static class EmployeeBuilder
	{
		public static void EmployeeBuild(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Employee>(entity =>
			{
				entity.Property(e => e.Id)
					.ValueGeneratedNever()
					.HasColumnName("employee_id");

				entity.Property(e => e.DeliveryPointId).HasColumnName("delivery_point_id");

				entity.Property(e => e.RoleId).HasColumnName("role_id");

				entity.Property(e => e.Email)
					.HasMaxLength(100)
					.HasColumnName("email");

				entity.Property(e => e.Name)
					.HasMaxLength(100)
					.HasColumnName("name");

				entity.Property(e => e.Password)
					.HasMaxLength(100)
					.HasColumnName("password");

				entity.Property(e => e.Phone)
					.HasMaxLength(100)
					.HasColumnName("phone");

				entity.Property(e => e.Salary)
					.HasColumnType("decimal(10, 2)")
					.HasColumnName("salary");

				entity.HasOne(d => d.DeliveryPoint)
					.WithMany(p => p.Employees)
					.HasForeignKey(d => d.DeliveryPointId)
					.HasConstraintName("FK__Employees__deliv__6FE99F9F")
					.OnDelete(DeleteBehavior.SetNull);
			});

			IPasswordHasher hasher = new PasswordHasher();

			modelBuilder.Entity<Employee>().HasData(
				new Employee { Id = 1, DeliveryPointId = 1, RoleId = 1, Name = "John Doe", Email = "john.doe@example.com",
					Password = hasher.HashPassword("123"), Phone = "123-456-7890", Salary = 28000.00m },
				new Employee { Id = 2, DeliveryPointId = 1, RoleId = 1, Name = "Jane Smith", Email = "jane.smith@example.com",
					Password = hasher.HashPassword("123"), Phone = "456-789-0123", Salary = 28000.00m },
				new Employee { Id = 3, DeliveryPointId = 2, RoleId = 2, Name = "Michael Johnson", Email = "michael.johnson@example.com",
					Password = hasher.HashPassword("123"), Phone = "789-012-3456", Salary = 28000.00m },
				new Employee { Id = 4, DeliveryPointId = 2, RoleId = 2, Name = "Emily Davis", Email = "emily.davis@example.com",
					Password = hasher.HashPassword("123"), Phone = "012-345-6789", Salary = 28000.00m },
				new Employee { Id = 5, DeliveryPointId = 3, RoleId = 2, Name = "William Wilson", Email = "william.wilson@example.com",
					Password = hasher.HashPassword("123"), Phone = "345-678-9012", Salary = 35000.00m },
				new Employee { Id = 6, DeliveryPointId = 3, RoleId = 2, Name = "Olivia Brown", Email = "olivia.brown@example.com",
					Password = hasher.HashPassword("123"), Phone = "678-901-2345", Salary = 35000.00m },
				new Employee { Id = 7, DeliveryPointId = 4, RoleId = 3, Name = "Daniel Lee", Email = "daniel.lee@example.com",
					Password = hasher.HashPassword("123"), Phone = "901-234-5678", Salary = 35000.00m },
				new Employee { Id = 8, DeliveryPointId = 4,RoleId = 3, Name = "Alexis Martinez", Email = "alexis.martinez@example.com",
					Password = hasher.HashPassword("123"), Phone = "234-567-8901", Salary = 35000.00m },
				new Employee { Id = 9, DeliveryPointId = 5, RoleId = 3, Name = "Grace Anderson", Email = "grace.anderson@example.com",
					Password = hasher.HashPassword("123"), Phone = "567-890-1234", Salary = 35000.00m },
				new Employee { Id = 10, DeliveryPointId = 5, RoleId = 3, Name = "Kevin Hernandez", Email = "kevin.hernandez@example.com",
					Password = hasher.HashPassword("123"), Phone = "890-123-4567", Salary = 35000.00m }
			);
		}

	}
}
