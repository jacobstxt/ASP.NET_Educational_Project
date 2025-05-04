using AutoMapper;
using Project_ASP.NET.Areas.Admin.Models.Products;
using Project_ASP.NET.Data.Entities;

namespace Project_ASP.NET.Areas.Admin.Mapper
{
    public class ProductsMapper:Profile
    {
        public ProductsMapper()
        {
          CreateMap<ProductEntity, ProductItemViewModel>()
            .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Name))
            .ForMember(x => x.Images, opt => opt.MapFrom(x => x.ProductImages.Select(x => x.FileName)));


          CreateMap<ProductCreateViewModel, ProductEntity>()
             .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId)) 
             .ForMember(dest => dest.ProductImages, opt => opt.Ignore());
        }

    }
}
