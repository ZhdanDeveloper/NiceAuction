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
            CreateMap<Product, ProductDTO>()
              .ForMember(p => p.CategoriesIds, c => c.MapFrom(x => x.ProductCategories.Select(i => i.CategoryId)))
              .ForMember(p => p.BidsIds, c => c.MapFrom(x => x.Orders.Select(b => b.Id)))
              .ForMember(p => p.Photo, opt => opt.Ignore())
              .ReverseMap();


            CreateMap<LoginDTO, UserDTO>()
                .ForMember(p=>p.UserName, c=>c.MapFrom(x=>x.Name))
                .ForMember(p => p.Password, c => c.MapFrom(x => x.Password)).ReverseMap();

            CreateMap<User, UserDTO>()      
             .ReverseMap();





        }

    }
}
