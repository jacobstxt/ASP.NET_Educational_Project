using AutoMapper;
using Project_ASP.NET.Data.Entities;
using Project_ASP.NET.Models.User;

namespace Project_ASP.NET.Mapper
{
    public class UserMapper:Profile
    {
        //public UserMapper() {
        //    CreateMap<UserSignUpViewModel, UserEntity>()
        //       .ForMember(dest => dest.Password, opt => opt.Ignore())
        //       .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.ViewAvatar ?? "/Picture/default.png"));
        //}
    }
}
