using AutoMapper;
using Project_ASP.NET.DataBase.Entities;
using Project_ASP.NET.Models.Category;

namespace Project_ASP.NET.Mapper
{
    public class CategoryMapper:Profile
    {
        public CategoryMapper() {
            CreateMap<CategoryEntity, CategoryItemViewModel>()
           .ForMember(x => x.Image, opt => opt.MapFrom(x => x.ImageUrl));

            CreateMap<CategoryCreateViewModel, CategoryEntity>()
            .ForMember(x => x.ImageUrl, opt => opt.Ignore());

        }
    }
}
