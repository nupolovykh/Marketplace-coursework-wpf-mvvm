using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.EntityFramework;
using System;

namespace MyWpfAppForDb.WPF.Inspectors
{
    public static class DatabaseInspector
    {
        public static Exception DatabaseValidation(IHost host, out Exception exeption)
        {
            try
            {
                var db = host.Services.GetRequiredService<AppDbContext>();
                if (!db.Database.CanConnect())
                {
                    throw new Exception("Have no connection");
                }
            }

            catch (Exception ex)
            {
                exeption = ex;
                return ex;
            }

            exeption = null!;
            return null!;
        }
    }
}
