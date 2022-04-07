using MGAuthentication.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MGAuthentication.Respositories.Common.JobPositionRepositories
{
    public interface IJobPositionRepository
    {
        IEnumerable<JobPosition> GetAll();

        IEnumerable<JobPosition> GetAllDeleted();

        Task<JobPosition> GetById(int jobPositionId);

        Task Create(JobPosition jobPosition);

        void Update(JobPosition jobPosition);

        void Delete(int jobPositionId);

        /// <summary>
        ///     Restores the data by altering IsDeleted field to true
        /// </summary>
        /// <param name="jobPositionId"></param>
        /// <returns></returns>
        Task<JobPosition> RestoreAsync(int jobPositionId);

        Task SaveChangesAsync();
    }
}