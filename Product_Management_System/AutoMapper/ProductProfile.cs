using AutoMapper;
using Product_Management_System.Data;
using Product_Management_System.Models;

namespace Product_Management_System.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
