using AutoMapper;
using Project_ASP.NET.Data.Entities;
using Project_ASP.NET.Data.Entities.Identity;
using Project_ASP.NET.Models.User;

namespace Project_ASP.NET.Mapper
{
    public class UserMapper:Profile
    {
        public UserMapper()
        {
            CreateMap<UserEntity, UserLinkViewModel>()
               .ForMember(x => x.Name, opt =>
               opt.MapFrom(x => $"{x.Surname} {x.Name}"))
                 .ForMember(x => x.Image, opt =>
                 opt.MapFrom(x => x.AvatarUrl ?? "default.webp"));

            CreateMap<UserSignUpViewModel, UserEntity>()
               .ForMember(x => x.AvatarUrl, opt =>
               opt.Ignore())
               .ForMember(x=>x.UserName, opt=> 
               opt.MapFrom(x=> x.Email));
                

        }
    }
}
