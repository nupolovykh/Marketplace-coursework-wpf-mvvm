using Microsoft.EntityFrameworkCore;
using Marketplace.EntityFramework.Entities;

namespace Marketplace.EntityFramework.EntitiesBuilders
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

			// Seed passwords are all "123" - demo data, documented as such in the
			// README. The hashes are hard-coded rather than computed here because
			// PBKDF2 salts are random: hashing inside OnModelCreating would make the
			// model differ on every run, which breaks change tracking and rules out
			// migrations entirely.
			modelBuilder.Entity<Employee>().HasData(
				new Employee { Id = 1, DeliveryPointId = 1, RoleId = 1, Name = "John Doe", Email = "john.doe@example.com",
					Password = "100000.3XFxzcuxhAeqHMnBBft7yg==.azobajlzbElBtrwKNO+a7O5bJuINiOKbb/Up+ButftM=", Phone = "123-456-7890", Salary = 28000.00m },
				new Employee { Id = 2, DeliveryPointId = 1, RoleId = 1, Name = "Jane Smith", Email = "jane.smith@example.com",
					Password = "100000.xrHnRzLTZH4rFChLpZzwyA==.SVAZ7CsrIvHpWPCyNudB1SkDIolpHfXM3VD2oxdr6Do=", Phone = "456-789-0123", Salary = 28000.00m },
				new Employee { Id = 3, DeliveryPointId = 2, RoleId = 2, Name = "Michael Johnson", Email = "michael.johnson@example.com",
					Password = "100000.+/8DkFAP1rBK25AKPaIlsw==.I8vTGwHvuCgGO3yIXlPyeR980jF8C2b37kCsiVbqgfI=", Phone = "789-012-3456", Salary = 28000.00m },
				new Employee { Id = 4, DeliveryPointId = 2, RoleId = 2, Name = "Emily Davis", Email = "emily.davis@example.com",
					Password = "100000.ox7O7V9qcjbhr7A2gaDkPA==.Ok+yFhOVzy817P9v2iF0zX7lWTB5DV8Do/e6rSxS2p4=", Phone = "012-345-6789", Salary = 28000.00m },
				new Employee { Id = 5, DeliveryPointId = 3, RoleId = 2, Name = "William Wilson", Email = "william.wilson@example.com",
					Password = "100000.vtaiWpi/onmzdXzZF7TNlA==.diQlxlff06/zkXQEOulwFmFs9nUMmYSiGPT1ZpYDIfg=", Phone = "345-678-9012", Salary = 35000.00m },
				new Employee { Id = 6, DeliveryPointId = 3, RoleId = 2, Name = "Olivia Brown", Email = "olivia.brown@example.com",
					Password = "100000.f0t3aQbGV9fxK2K/XUBsWw==.3C78XRZtqUt8XR0w39srypmqAVveR2vykwW32ykmESM=", Phone = "678-901-2345", Salary = 35000.00m },
				new Employee { Id = 7, DeliveryPointId = 4, RoleId = 3, Name = "Daniel Lee", Email = "daniel.lee@example.com",
					Password = "100000.cmwNkcYzMx980AYbCcnh6Q==.4TvhNjTvEJVPSore1ODREM+yFVfLjcLOKQGBbzRyUN8=", Phone = "901-234-5678", Salary = 35000.00m },
				new Employee { Id = 8, DeliveryPointId = 4,RoleId = 3, Name = "Alexis Martinez", Email = "alexis.martinez@example.com",
					Password = "100000.TBnoKpr6GwUTlR1z3eZA+Q==.3nTqXhq+y55qzZO04HVUobcKtdSOnhXwSrQrkm73FBk=", Phone = "234-567-8901", Salary = 35000.00m },
				new Employee { Id = 9, DeliveryPointId = 5, RoleId = 3, Name = "Grace Anderson", Email = "grace.anderson@example.com",
					Password = "100000.LFFYjibiRT42UOLZF+CRSQ==.7ky0X6rQOXKcEPYwRqQWxP6PdU4WK1JlDsDrlTdoHWM=", Phone = "567-890-1234", Salary = 35000.00m },
				new Employee { Id = 10, DeliveryPointId = 5, RoleId = 3, Name = "Kevin Hernandez", Email = "kevin.hernandez@example.com",
					Password = "100000.H4fmWC+w6oDtMTdT6b8xdQ==.M0/ElxVnJGDW6a6mDtJ9ktqh456/fYeODqXAlEH2/rU=", Phone = "890-123-4567", Salary = 35000.00m }
			);
		}

	}
}
