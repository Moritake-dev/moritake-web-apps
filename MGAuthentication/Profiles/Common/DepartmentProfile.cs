using AutoMapper;
using MGAuthentication.Data.Common;
using MGAuthentication.Data.ViewModels.Common.Department;

namespace MGAuthentication.Profiles.Common
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            // source -> destination
            CreateMap<Department, DepartmentReadVM>();
            CreateMap<DepartmentCreateVM, Department>();
            CreateMap<DepartmentReadVM, DepartmentUpdateVM>();
            CreateMap<DepartmentUpdateVM, Department>();
        }
    }
}