using AutoMapper;
using MGAuthentication.Data.Common;
using MGAuthentication.Data.DTOs.Common;
using MGAuthentication.Data.DTOs.InformationBoardDTOs;
using MGAuthentication.Data.RepositoryModels;
using MGAuthentication.Data.User;

namespace MGAuthentication.Profiles.Common
{
    public class InformationBoardProfile : Profile
    {
        public InformationBoardProfile()
        {
            // source -> destination
            CreateMap<InformationBoardRM, InformationBoardReadDto>();
            CreateMap<InformationBoardCreateDto, InformationBoard>();
            CreateMap<InformationBoard, InformationBoardReadDto>().ForMember(dest => dest.UserInfo, act => act.MapFrom(src => src.ApplicationUser));
            CreateMap<ApplicationUser, UserReadDto>().ReverseMap();
            CreateMap<InformationBoardRM, InformationBoardUpdateDto>();
            CreateMap<InformationBoardUpdateDto, InformationBoardRM>();
            CreateMap<InformationBoardUpdateDto, InformationBoard>();
        }
    }
}