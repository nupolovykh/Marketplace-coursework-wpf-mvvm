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
                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("employee_id");

                entity.Property(e => e.DeliveryPointId).HasColumnName("delivery_point_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .HasColumnName("phone");

                entity.Property(e => e.Salary)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("salary");

                entity.HasOne(d => d.DeliveryPoint)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DeliveryPointId)
                    .HasConstraintName("FK__Employees__deliv__6FE99F9F");
            });

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, DeliveryPointId = 1, Name = "John Doe", Email = "john.doe@example.com", Phone = "123-456-7890", Salary = 28000.00m },
                new Employee { EmployeeId = 2, DeliveryPointId = 1, Name = "Jane Smith", Email = "jane.smith@example.com", Phone = "456-789-0123", Salary = 28000.00m },
                new Employee { EmployeeId = 3, DeliveryPointId = 2, Name = "Michael Johnson", Email = "michael.johnson@example.com", Phone = "789-012-3456", Salary = 28000.00m },
                new Employee { EmployeeId = 4, DeliveryPointId = 2, Name = "Emily Davis", Email = "emily.davis@example.com", Phone = "012-345-6789", Salary = 28000.00m },
                new Employee { EmployeeId = 5, DeliveryPointId = 3, Name = "William Wilson", Email = "william.wilson@example.com", Phone = "345-678-9012", Salary = 35000.00m },
                new Employee { EmployeeId = 6, DeliveryPointId = 3, Name = "Olivia Brown", Email = "olivia.brown@example.com", Phone = "678-901-2345", Salary = 35000.00m },
                new Employee { EmployeeId = 7, DeliveryPointId = 4, Name = "Daniel Lee", Email = "daniel.lee@example.com", Phone = "901-234-5678", Salary = 35000.00m },
                new Employee { EmployeeId = 8, DeliveryPointId = 4, Name = "Alexis Martinez", Email = "alexis.martinez@example.com", Phone = "234-567-8901", Salary = 35000.00m },
                new Employee { EmployeeId = 9, DeliveryPointId = 5, Name = "Grace Anderson", Email = "grace.anderson@example.com", Phone = "567-890-1234", Salary = 35000.00m },
                new Employee { EmployeeId = 10, DeliveryPointId = 5, Name = "Kevin Hernandez", Email = "kevin.hernandez@example.com", Phone = "890-123-4567", Salary = 35000.00m }
            );
        }

    }
}
