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
            CreateMap<Auction, AuctionDTO>()
              .ForMember(p => p.CategoriesIds, c => c.MapFrom(x => x.AuctionCategories.Select(i => i.CategoryId)))
              .ForMember(p => p.BidsIds, c => c.MapFrom(x => x.Bids.Select(b => b.Id)))
              .ForMember(p => p.Photo, opt => opt.Ignore())
              .ReverseMap();




        }

    }
}
