using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<CreateProductDTO, Product>()
                 .ForMember(p => p.Id, opt => opt.Ignore());

            CreateMap<Product, CreateProductDTO>()
             .ForMember(p => p.CategoriesIds, c => c.MapFrom(x => x.ProductCategories.Select(i => i.CategoryId)));

            CreateMap<Product, ReadProductDTO>()
               .ForMember(p => p.CategoriesIds, c => c.MapFrom(x => x.ProductCategories.Select(i => i.CategoryId)))
              .ForMember(p => p.Photo, c => c.MapFrom(x => x.PhotoPath))
              .ForMember(p=>p.OwnerName, c=>c.MapFrom(x=>x.User.UserName))
              .ForMember(p => p.OwnerPhone, c => c.MapFrom(x => x.User.Phone));


            CreateMap<UpdateProductDTO, Product>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Category, CreateCategoryDTO>().ReverseMap();

            CreateMap<LoginDTO, ReadUserDTO>()
                .ForMember(p => p.UserName, c => c.MapFrom(x => x.Name));

            CreateMap<User, ReadUserDTO>();

            CreateMap<User, LoginDTO>()
              .ForMember(x => x.Name, c => c.MapFrom(x => x.UserName));

            CreateMap<CreateUserDTO, User>()
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ReadUserDTO, User>()
             .ForMember(p => p.Id, opt => opt.Ignore());

            CreateMap<CreateOrderDTO, Order>();

            CreateMap<Order, ReadOrderDTO>()
            .ForMember(x => x.TotalPrice, c => c.MapFrom(x => x.Product.Price * x.Amount))
            .ForMember(x => x.ProductName, c => c.MapFrom(x => x.Product.Name))
            .ForMember(x => x.CustomerName, c => c.MapFrom(x => x.User.FirstName))
            .ForMember(x => x.CustomerPhone, c => c.MapFrom(x => x.User.Phone));

            CreateMap<Order, OutcomingOrderDTO>()
           .ForMember(x => x.TotalPrice, c => c.MapFrom(x => x.Product.Price * x.Amount))
           .ForMember(x => x.ProductName, c => c.MapFrom(x => x.Product.Name))
           .ForMember(x => x.OwnerName, c => c.MapFrom(x => x.Product.User.FirstName))
           .ForMember(x => x.OwnerPhone, c => c.MapFrom(x => x.Product.User.Phone));



        }

    }
}
