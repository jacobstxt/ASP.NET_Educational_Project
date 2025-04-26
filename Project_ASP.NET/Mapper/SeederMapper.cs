using AutoMapper;
using Project_ASP.NET.Data.Entities;
using Project_ASP.NET.Models.Seeder;

namespace Project_ASP.NET.Mapper
{
    public class SeederMapper:Profile
    {
        public SeederMapper()
        {
            CreateMap<SeederCategoryModal, CategoryEntity>()
            .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Image));
        }
    }
}
