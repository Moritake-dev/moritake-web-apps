using AutoMapper;
using MGAuthentication.Data.Common;
using MGAuthentication.Data.ViewModels.Common.JobPosition;

namespace MGAuthentication.Profiles.Common
{
    public class JobPositionProfile : Profile
    {
        public JobPositionProfile()
        {
            // source -> destination
            CreateMap<JobPositionCreateVM, JobPosition>();
            CreateMap<JobPosition, JobPositionReadVM>();
            CreateMap<JobPositionReadVM, JobPositionUpdateVM>();
            CreateMap<JobPositionUpdateVM, JobPosition>();
        }
    }
}