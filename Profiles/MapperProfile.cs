using AutoMapper;
using Catalog.Data;
using Catalog.DTO;
using Catalog.Models;
using System;

namespace Catalog.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //_categoryRepo = categoryRepo;
           // CreateMap<Product, ProductDTO>().ForMember(dest => dest.category, opt => opt.MapFrom(src => _categoryRepo.GetCategory(src.CategoryId)));
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, ProductAuthDTO>();
            CreateMap<ProductDTO, Product>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<Account, AccountGetDTO>();
            CreateMap<AccountAuthDTO, Account>();
        }
    }

}