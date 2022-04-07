using AutoMapper;
using MGAuthentication.Data.User;
using MGAuthentication.Data.ViewModels.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGAuthentication.Profiles
{
    public class CurrentLocationProfile : Profile
    {
        public CurrentLocationProfile()
        {
            // source -> destination

            CreateMap<CurrentLocation, LocationEditVM>();
        }
    }
}