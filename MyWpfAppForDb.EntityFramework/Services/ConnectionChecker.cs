using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.EntityFramework;
using System;

namespace MyWpfAppForDb.EntityFramework.Services
{
    public static class ConnectionChecker
    {
        public static Exception DatabaseValidation(IHost host, out Exception exeption)
        {
            try
            {
                var db = host.Services.GetRequiredService<AppDbContext>();

                if (!db.Database.CanConnect())
                {
                    throw new Exception("The Application has not the connection with database. Reload the whole application.");
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
