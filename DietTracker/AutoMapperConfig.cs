using AutoMapper;
using BusinessLogic.Models;
using DataAccessLayer.Entities;
using DietTracker.Models;

namespace DietTracker
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<WeightViewModel, DataToCalculate>();
            CreateMap<AddProductViewModel, Product>().ForMember(dest => dest.ProductName, 
                opt => opt.MapFrom(src => src.NewProductName));
            CreateMap<Product, AddProductViewModel>();

        }
    }
}
