using AutoMapper;
using WSApp.Src.Application.DTOs;
using WSApp.Src.Domain.Entities;

namespace WSApp.Src.Application.Configurations.ProfileMaps
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
