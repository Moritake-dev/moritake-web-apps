using MGAuthentication.Data.ViewModels.Common.JobPosition;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MGAuthentication.Services.CommonServices.JobPositionServices
{
    public interface IJobPositionService
    {
        IEnumerable<JobPositionReadVM> GetAllJobPositions();

        IEnumerable<JobPositionReadVM> GetAllDeletedJobPositions();

        Task<JobPositionReadVM> GetJobPositionById(int jobPositionId);

        Task<JobPositionReadVM> Create(JobPositionCreateVM jobPosition);

        Task UpdateAsync(int jobPositionId, JobPositionUpdateVM jobPositionUpdateVM);

        Task<JobPositionReadVM> RestoreAsync(int jobPositionId);

        Task Delete(int jobPositionId);
    }
}