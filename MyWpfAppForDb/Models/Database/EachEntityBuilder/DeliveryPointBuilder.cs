using MyWpfAppForDb.Models.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace MyWpfAppForDb.Models.Database.EachEntityBuilder
{
    internal static class DeliveryPointBuilder
    {
        public static void DeliveryPointBuild(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeliveryPoint>(entity =>
            {
                entity.ToTable("Delivery_Points");

                entity.Property(e => e.DeliveryPointId)
                    .ValueGeneratedNever()
                    .HasColumnName("delivery_point_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .HasColumnName("city");

                entity.Property(e => e.Rating)
                    .HasColumnType("decimal(3, 2)")
                    .HasColumnName("rating");

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(20)
                    .HasColumnName("zipcode");
            });

            modelBuilder.Entity<DeliveryPoint>().HasData(
                new DeliveryPoint { DeliveryPointId = 1, Address = "123 Main Street", City = "Anytown", Rating = 0.00m, Zipcode = "12345" },
                new DeliveryPoint { DeliveryPointId = 2, Address = "456 Elm Street", City = "Othertown", Rating = 2.73m, Zipcode = "67890" },
                new DeliveryPoint { DeliveryPointId = 3, Address = "789 Oak Street", City = "Anycity", Rating = 4.51m, Zipcode = "13579" },
                new DeliveryPoint { DeliveryPointId = 4, Address = "321 Pine Avenue", City = "Sometown", Rating = 5.00m, Zipcode = "24680" },
                new DeliveryPoint { DeliveryPointId = 5, Address = "555 Maple Drive", City = "Anyville", Rating = 3.56m, Zipcode = "97531" }
            );

        }
    }
}
