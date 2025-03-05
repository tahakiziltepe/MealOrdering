﻿using AutoMapper;
using MealOrdering.Server.Data.Models;
using MealOrdering.Shared.DTO;

namespace MealOrdering.Services.Extensions
{
	public static class ConfigureMappingExtension
	{
		public static IServiceCollection ConfigureMapping(this IServiceCollection service)
		{
			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MappingProfile());
			});

			IMapper mapper = mappingConfig.CreateMapper();
			service.AddSingleton(mapper);
			return service;
		}
	}
	
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			AllowNullDestinationValues = true;
			AllowNullCollections = true;

			CreateMap<Suppliers, SupplierDTO>().ReverseMap();
			
			CreateMap<Users, UserDTO>().ReverseMap();

			CreateMap<Orders, OrderDTO>()
				.ForMember(x => x.SupplierName, y => y.MapFrom(z => z.Supplier.Name))
				.ForMember(x => x.CreatedUserFullName, y => y.MapFrom(z => z.CreatedUser.FirstName + " " + z.CreatedUser.LastName));

			CreateMap<OrderDTO, Orders>();

			CreateMap<OrderItems, OrderItemsDTO>()
				.ForMember(x => x.OrderName, y => y.MapFrom(z => z.Order.Name == ""))
				.ForMember(x => x.CreatedUserFullName, y => y.MapFrom(z => z.CreatedUser.FirstName + " " + z.CreatedUser.LastName));

			CreateMap<OrderItemsDTO, OrderItems>();
		}
	}
}
