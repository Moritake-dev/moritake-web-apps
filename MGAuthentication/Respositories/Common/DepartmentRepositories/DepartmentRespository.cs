using MGAuthentication.Data;
using MGAuthentication.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGAuthentication.Respositories.Common.DepartmentRepositories
{
    public class DepartmentRespository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRespository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Department department)
        {
            await _context.AddAsync(department);
        }

        public void Delete(int departmentId)
        {
            var result = _context.Departments.Find(departmentId);
            _context.Remove(result);
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.Where(x => x.IsDeleted == false).AsEnumerable();
        }

        public async Task<Department> GetById(int departmentId)
        {
            var result = await _context.Departments.FindAsync(departmentId);
            return result;
        }

        public IEnumerable<Department> GetAllDeleted()
        {
            return _context.Departments.Where(x => x.IsDeleted == true).AsEnumerable();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Department department)
        {
            // NO NEED TO UPDATE. AUTOMAPPER WILL DO THE JOB FOR US IN THE SERVICE LEVEL
        }

        public async Task<Department> RestoreAsync(int departmentId)
        {
            var result = await _context.Departments.FindAsync(departmentId);
            result.IsDeleted = false;
            await _context.SaveChangesAsync();
            return result;
        }
    }
}