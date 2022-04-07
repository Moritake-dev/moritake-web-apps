using AutoMapper;
using MGAuthentication.Data.Common;
using MGAuthentication.Data.ViewModels.Common.JobPosition;
using MGAuthentication.Respositories.Common.JobPositionRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGAuthentication.Services.CommonServices.JobPositionServices
{
    public class JobPositionService : IJobPositionService
    {
        private readonly IJobPositionRepository _repository;
        private readonly IMapper _mapper;

        public JobPositionService(IJobPositionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<JobPositionReadVM> Create(JobPositionCreateVM jobPosition)
        {
            var model = _mapper.Map<JobPosition>(jobPosition);
            await _repository.Create(model);
            await _repository.SaveChangesAsync();
            return (_mapper.Map<JobPositionReadVM>(model));
        }

        public async Task Delete(int jobPositionId)
        {
            _repository.Delete(jobPositionId);
            await _repository.SaveChangesAsync();
        }

        public IEnumerable<JobPositionReadVM> GetAllDeletedJobPositions()
        {
            var result = _repository.GetAllDeleted().ToList();
            // TODO : Mapper profile config
            return _mapper.Map<IEnumerable<JobPositionReadVM>>(result);
        }

        public IEnumerable<JobPositionReadVM> GetAllJobPositions()
        {
            var result = _repository.GetAll().OrderBy(x => x.Id).ToList();
            return (_mapper.Map<IEnumerable<JobPositionReadVM>>(result));
        }

        public async Task<JobPositionReadVM> GetJobPositionById(int jobPositionId)
        {
            var result = await _repository.GetById(jobPositionId);
            return _mapper.Map<JobPositionReadVM>(result);
        }

        public async Task UpdateAsync(int jobPositionId, JobPositionUpdateVM jobPositionUpdateVM)
        {
            var jobPositionModel = await _repository.GetById(jobPositionId);
            _mapper.Map(jobPositionUpdateVM, jobPositionModel);

            await _repository.SaveChangesAsync();
        }

        public async Task<JobPositionReadVM> RestoreAsync(int jobPositionId)
        {
            var restoredModel = await _repository.RestoreAsync(jobPositionId);
            var result = _mapper.Map<JobPositionReadVM>(restoredModel);
            return result;
        }
    }
}