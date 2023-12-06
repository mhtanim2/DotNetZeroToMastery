using AutoMapper;
using E_Commerce.Models.Domain;
using E_Commerce.Models.Dto.Request;
using E_Commerce.Models.Dto.Response;

namespace E_Commerce.Helper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductRequestDto>().ReverseMap();
            CreateMap<Basket, BasketRequestDto>().ReverseMap();
            CreateMap<Basket, BasketDto>().ReverseMap();

        }
    }
}
