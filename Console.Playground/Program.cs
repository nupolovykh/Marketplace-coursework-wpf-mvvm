using Playground;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyWpfAppForDb.EntityFramework;
using System.Diagnostics;

var stopWatch = Stopwatch.StartNew();

var builder = new ConfigurationBuilder();

builder.SetBasePath(PathBuilder.CreatingPath())
	   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration config = builder.Build();

string connect = PathBuilder.GetDataSource(config);

DbContextOptionsBuilder<AppDbContext> options = new();
Action<DbContextOptionsBuilder> confige = (o) => o.UseSqlite(connect);
confige(options);

using (AppDbContext db = new(options.Options))
{
	db.Database.EnsureCreatedAsync().Wait();
	Console.WriteLine("All ready!");
}

stopWatch.Stop();

Console.WriteLine($"database creation and attuning => {stopWatch.Elapsed}");