﻿using AutoMapper;
using Project_ASP.NET.Data.Entities;
using Project_ASP.NET.Models.Category;
using Project_ASP.NET.Models.Helpers;

namespace Project_ASP.NET.Mapper
{
    public class CategoryMapper:Profile
    {
        public CategoryMapper() {
            CreateMap<CategoryEntity, CategoryItemViewModel>()
           .ForMember(x => x.Image, opt 
           => opt.MapFrom(x => x.ImageUrl));

            CreateMap<CategoryCreateViewModel, CategoryEntity>()
            .ForMember(x => x.ImageUrl, opt => opt.Ignore());

            CreateMap<CategoryEntity, CategoryEditViewModel>()
            .ForMember(x => x.ViewImage, opt => opt.MapFrom(x => string.IsNullOrEmpty(x.ImageUrl) ? "/Picture/default.png" : $"/images/400_{x.ImageUrl}"))
            .ForMember(x => x.ImageFile, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<CategoryEntity, SelectItemViewModel>();

        }
    }
}
