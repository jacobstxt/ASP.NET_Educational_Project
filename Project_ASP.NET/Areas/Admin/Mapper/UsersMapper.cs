using AutoMapper;
using Project_ASP.NET.Areas.Admin.Models.Users;
using Project_ASP.NET.Data.Entities.Identity;

namespace Project_ASP.NET.Areas.Admin.Mapper
{
    public class UsersMapper:Profile
    {
        public UsersMapper() {
            CreateMap<UserEntity, UserItemViewModel>()
              .ForMember(x => x.Image, opt => opt.MapFrom(x => x.AvatarUrl))
              .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.Name))
              .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.Surname))
              .ReverseMap();
        
        }
    }
}
