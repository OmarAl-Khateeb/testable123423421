using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Address, AddressDto>().ReverseMap();
            
            CreateMap<User, UserToReturnDto>()
                .ForMember(d => d.UserType, o => o.MapFrom(s => s.UserType.Name))
                .ForMember(d => d.Province, o => o.MapFrom(s => s.Province.Name));
            CreateMap<UserUpdateDto, User>();
                
            CreateMap<Branch, BranchToReturnDto>()
                .ForMember(d => d.Province, o => o.MapFrom(s => s.Province.Name));
            CreateMap<BranchUpdateDto, Branch>();
            CreateMap<Slider, SliderDto>().ReverseMap();
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<CommonQuestion, CommonQuestionDto>().ReverseMap();
            CreateMap<NewsDto, News>().ReverseMap();
                
        }
    }
}