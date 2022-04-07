using AutoMapper;
using MGAuthentication.Data.ViewModels.User;
using MGAuthentication.Data.ViewModels.UserProfile;

namespace MGAuthentication.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // source -> destination

            CreateMap<UserReadVM, ProfileReadVM>();
            CreateMap<UserReadVM, ProfileUpdateVM>();
        }
    }
}