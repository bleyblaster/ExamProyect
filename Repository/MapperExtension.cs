using AutoMapper;
using Domain.Model;
using Microsoft.Extensions.DependencyInjection;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class MapperExtension
    {
        public static void AddMapperExtension(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<User, UserModel>().ReverseMap();
                mc.CreateMap<User, EditUserModel>().ReverseMap();
                mc.CreateMap<Product, ProductModel>().ReverseMap();
                mc.CreateMap<Product, EditProductModel>().ReverseMap();
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
