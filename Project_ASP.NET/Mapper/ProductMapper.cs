using AutoMapper;
using Project_ASP.NET.Data.Entities;
using Project_ASP.NET.Models.Products;

namespace Project_ASP.NET.Mapper
{
    public class ProductMapper:Profile
    {
        public ProductMapper() {
            CreateMap<ProductEntity, ProductItemViewModel>()
             .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Name))
             .ForMember(x => x.Images, opt => opt.MapFrom(x => x.ProductImages.Select(x => x.FileName)));
        }
    }
}
