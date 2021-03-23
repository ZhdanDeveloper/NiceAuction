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
              .ForMember(p=>p.OwnerName, c=>c.MapFrom(x=>x.User.UserName));

            CreateMap<UpdateProductDTO, Product>()
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
              

            CreateMap<LoginDTO, UserDTO>()
                .ForMember(p=>p.UserName, c=>c.MapFrom(x=>x.Name))
                .ForMember(p => p.Password, c => c.MapFrom(x => x.Password)).ReverseMap();

            CreateMap<User, UserDTO>();
               
            CreateMap<UserDTO, User>()
             .ForMember(p => p.Id, opt => opt.Ignore());

        }

    }
}
