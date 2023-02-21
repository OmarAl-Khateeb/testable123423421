using API.Dtos;
using AutoMapper;
using Core.Entities;

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
            
            CreateMap<User, UserToReturnDto>()
                .ForMember(d => d.UserType, o => o.MapFrom(s => s.UserType.Name))
                .ForMember(d => d.Province, o => o.MapFrom(s => s.Province.Name));
            CreateMap<UserUpdateDto, User>();
                
            CreateMap<Branch, BranchToReturnDto>()
                .ForMember(d => d.Province, o => o.MapFrom(s => s.Province.Name));
            CreateMap<BranchUpdateDto, Branch>();
            CreateMap<Slider, SliderDto>();
            CreateMap<SliderDto, Slider>();
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();
            CreateMap<CommonQuestion, CommonQuestionDto>();
            CreateMap<CommonQuestionDto, CommonQuestion>();
                
        }
    }
}