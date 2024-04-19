using AppWPF.Models.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppWPF.Models.Database.EachEntityBuilder
{
    internal static class MarketBuilder
    {
        public static void MarketBuild(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Market>(entity =>
            {
                entity.Property(e => e.MarketId)
                    .ValueGeneratedNever()
                    .HasColumnName("market_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .HasColumnName("city");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Market>().HasData(
                new Market { MarketId = 1, Name = "City Central Market", City = "New York", Address = "123 Main Street" },
                new Market { MarketId = 2, Name = "Fresh Fare Marketplace", City = "Los Angeles", Address = "456 Elm Street" },
                new Market { MarketId = 3, Name = "Urban Gourmet Market", City = "Chicago", Address = "789 Oak Street" },
                new Market { MarketId = 4, Name = "Pacific Coast Marketplace", City = "Houston", Address = "101 Pine Street" },
                new Market { MarketId = 5, Name = "Sunrise Valley Market", City = "Miami", Address = "202 Maple Street" }
            );
        }
    }
}
