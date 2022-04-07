using MGAuthentication.Data;
using MGAuthentication.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGAuthentication.Respositories.Common.JobPositionRepositories
{
    public class JobPositionRepository : IJobPositionRepository
    {
        private readonly ApplicationDbContext _context;

        public JobPositionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(JobPosition jobPosition)
        {
            await _context.AddAsync(jobPosition);
        }

        public void Delete(int jobPositionId)
        {
            var result = _context.JobPositions.Find(jobPositionId);
            _context.Remove(result);
        }

        public IEnumerable<JobPosition> GetAll()
        {
            return _context.JobPositions.Where(x => x.IsDeleted == false).AsEnumerable();
        }

        public IEnumerable<JobPosition> GetAllDeleted()
        {
            return _context.JobPositions.Where(x => x.IsDeleted == true).AsEnumerable();
        }

        public async Task<JobPosition> GetById(int jobPositionId)
        {
            var result = await _context.JobPositions.FindAsync(jobPositionId);
            return result;
        }

        public async Task<JobPosition> RestoreAsync(int jobPositionId)
        {
            var result = await _context.JobPositions.FindAsync(jobPositionId);
            result.IsDeleted = false;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(JobPosition jobPosition)
        {
            // NO NEED TO UPDATE. AUTOMAPPER WILL DO THE JOB FOR US IN THE SERVICE LEVEL
        }
    }
}