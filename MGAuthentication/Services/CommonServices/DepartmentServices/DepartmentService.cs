using AutoMapper;
using MGAuthentication.Data.Common;
using MGAuthentication.Data.ViewModels.Common.Department;
using MGAuthentication.Respositories.Common.DepartmentRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGAuthentication.Services.CommonServices.DepartmentServices
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DepartmentReadVM> Create(DepartmentCreateVM department)
        {
            var model = _mapper.Map<Department>(department);
            await _repository.Create(model);
            await _repository.SaveChangesAsync();
            return (_mapper.Map<DepartmentReadVM>(model));
        }

        public async Task Delete(int departmentId)
        {
            _repository.Delete(departmentId);
            await _repository.SaveChangesAsync();
        }

        public IEnumerable<DepartmentReadVM> GetAllDeletedDepartments()
        {
            var result = _repository.GetAllDeleted().ToList();
            return _mapper.Map<IEnumerable<DepartmentReadVM>>(result);
        }

        public IEnumerable<DepartmentReadVM> GetAllDepartments()
        {
            var result = _repository.GetAll().ToList();
            return (_mapper.Map<IEnumerable<DepartmentReadVM>>(result));
        }

        public async Task<DepartmentReadVM> GetDepartmentById(int departmentId)
        {
            var result = await _repository.GetById(departmentId);
            return _mapper.Map<DepartmentReadVM>(result);
        }

        public async Task UpdateAsync(int departmentId, DepartmentUpdateVM departmentUpdateVM)
        {
            var departmentModel = await _repository.GetById(departmentId);
            _mapper.Map(departmentUpdateVM, departmentModel);

            await _repository.SaveChangesAsync();
        }

        public async Task<DepartmentReadVM> RestoreAsync(int departmentId)
        {
            var restoredModel = await _repository.RestoreAsync(departmentId);
            var result = _mapper.Map<DepartmentReadVM>(restoredModel);
            return result;
        }
    }
}