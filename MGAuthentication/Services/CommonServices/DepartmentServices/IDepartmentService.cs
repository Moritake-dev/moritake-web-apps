using MGAuthentication.Data.ViewModels.Common.Department;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MGAuthentication.Services.CommonServices.DepartmentServices
{
    /// <summary>
    ///     CRUD of department
    /// </summary>
    public interface IDepartmentService
    {
        /// <summary>
        ///     Gets All Departments registered in the application
        /// </summary>
        /// <returns></returns>
        IEnumerable<DepartmentReadVM> GetAllDepartments();

        IEnumerable<DepartmentReadVM> GetAllDeletedDepartments();

        Task<DepartmentReadVM> GetDepartmentById(int departmentId);

        Task<DepartmentReadVM> Create(DepartmentCreateVM department);

        Task UpdateAsync(int departmentId, DepartmentUpdateVM departmentUpdateVM);

        Task<DepartmentReadVM> RestoreAsync(int departmentId);

        Task Delete(int departmentId);
    }
}