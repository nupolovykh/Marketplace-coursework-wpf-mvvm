using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWpfAppForDb.EntityFramework.Entities;
using MyWpfAppForDb.WPF.Models.DataTransferObjects;

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
			CreateMap<Category, CategoryDto>();
			CreateMap<Client, ClientDto>();
			CreateMap<DeliveryPoint, DeliveryPointDto>();
			CreateMap<Employee, EmployeeDto>();
			CreateMap<Market, MarketDto>();
			CreateMap<Order, OrderDto>();
			CreateMap<OrdersItem, OrdersItemDto>();
			CreateMap<Product, ProductDto>();
			CreateMap<ProductsInstance, ProductsInstanceDto>();
		}
	}

}
