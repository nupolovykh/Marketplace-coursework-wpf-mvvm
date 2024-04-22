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
            host.ConfigureServices((services) =>
            {
                services.AddAutoMapper(typeof(OrganizationProfile));
            });

            return host;
        }
    }

    internal class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Category, CategoryGto>();
        }
    }

}
