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
			CreateMap<Category, CategoryDto>().ReverseMap();

			CreateMap<Client, ClientDto>().ReverseMap();

			CreateMap<DeliveryPoint, DeliveryPointDto>().ReverseMap();

			CreateMap<Employee, EmployeeDto>().ReverseMap();

			CreateMap<Market, MarketDto>().ReverseMap();

			CreateMap<Order, OrderDto>().ReverseMap();

			CreateMap<OrdersItem, OrdersItemDto>().ReverseMap();

			CreateMap<Product, ProductDto>().ReverseMap();

			CreateMap<ProductsInstance, ProductsInstanceDto>().ReverseMap();

			CreateMap<Role, RoleDto>().ReverseMap();
		}
	}

}
