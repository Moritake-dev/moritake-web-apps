using MGAuthentication.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MGAuthentication.Respositories.Common.DepartmentRepositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();

        IEnumerable<Department> GetAllDeleted();

        Task<Department> GetById(int departmentId);

        Task Create(Department department);

        void Update(Department department);

        void Delete(int departmentId);

        Task<Department> RestoreAsync(int departmentId);

        Task SaveChangesAsync();
    }
}