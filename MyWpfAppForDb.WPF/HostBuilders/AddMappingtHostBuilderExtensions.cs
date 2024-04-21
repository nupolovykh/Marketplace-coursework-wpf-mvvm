using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.WPF.Models.ModelEntities;
using System;

namespace MyWpfAppForDb.WPF.HostBuilders
{
    internal static class AddMappingHostBuilderExtensions
    {
        public static IHostBuilder AddMapping(this IHostBuilder host)
        {
            Action<IMapperConfigurationExpression> action = (cnf) =>
            {
                cnf.CreateMap<Category, CategoryDisplay>();
                cnf.CreateMap<Client, ClientDisplay>();
            };

            host.ConfigureServices((services) =>
            {
                services.AddAutoMapper(action);
            });

            return host;
        }
    }
}
