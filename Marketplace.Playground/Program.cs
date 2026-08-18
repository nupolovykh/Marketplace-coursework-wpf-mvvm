using Marketplace.Playground;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Marketplace.EntityFramework;
using System.Diagnostics;

// Scratch project kept from development: it creates the SQLite database from the
// model without starting the UI, and hosts the async/threading experiments in
// AsyncExamples.cs.

var stopWatch = Stopwatch.StartNew();

IConfiguration config = ConfigurationLoader.Load();

string connectionString = ConfigurationLoader.GetConnectionString(config);

DbContextOptionsBuilder<AppDbContext> options = new();
Action<DbContextOptionsBuilder> configure = (o) => o.UseSqlite(connectionString);
configure(options);

using (AppDbContext db = new(options.Options))
{
	db.Database.EnsureCreatedAsync().Wait();
	Console.WriteLine("All ready!");
}

stopWatch.Stop();

Console.WriteLine($"database creation and tuning => {stopWatch.Elapsed}");
