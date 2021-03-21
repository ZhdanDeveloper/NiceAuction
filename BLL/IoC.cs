using AutoMapper;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BLL
{
    public static class IoC
    {

        public static void AddServices(this IServiceCollection services)
        {

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<FileManager>();

        }



        public static void AddAutoMapper(this IServiceCollection services)
        {

            var MappingConfig = new MapperConfiguration(x => x.AddProfile(new AutomapperProfile()));
            IMapper mapper = MappingConfig.CreateMapper();
            services.AddSingleton(mapper);

        }
    }
}
