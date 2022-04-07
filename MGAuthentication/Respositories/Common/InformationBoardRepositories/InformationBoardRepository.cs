using AutoMapper;
using MGAuthentication.Data;
using MGAuthentication.Data.Common;
using MGAuthentication.Data.DTOs.Common;
using MGAuthentication.Data.DTOs.InformationBoardDTOs;
using MGAuthentication.Data.RepositoryModels;
using MGAuthentication.Data.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGAuthentication.Respositories.Common.InformationBoardRepositories
{
    public class InformationBoardRepository : IInformationBoardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public InformationBoardRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Create(InformationBoard informationBoard)
        {
            await _context.AddAsync(informationBoard);
        }

        public async Task Delete(int infoId)
        {
            var result = _context.InformationBoard.Find(infoId);
            result.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public IEnumerable<InformationBoardRM> GetAll()
        {
            var result = _context.InformationBoard.Where(x => x.IsDeleted == false)
                .Select(x => new InformationBoardRM
                {
                    Id = x.Id,
                    MessageTitle = x.MessageTitle,
                    MessageDetail = x.MessageDetail,
                    UserId = x.UserId,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    UpdatedDate = x.UpdatedDate,
                    UpdatedBy = x.UpdatedBy,
                    UserInfo = _context.Users.Where(v => v.Id == x.UserId).Select(v => new UserReadDto
                    {
                        FirstName = v.FirstName,
                        LastName = v.LastName,
                        DateOfBirth = v.DateOfBirth,
                        Department = _context.Departments.Where(vx => vx.Id == v.DepartmentId).FirstOrDefault(),
                        JobPosition = _context.JobPositions.Where(vx => vx.Id == v.JobPositionId).FirstOrDefault(),
                        EmergencyContactNumber = v.EmergencyContactNumber,
                        Gender = v.Gender,
                    }).FirstOrDefault()
                }).ToList();
            return result;
        }

        public IEnumerable<InformationBoard> GetAllDeleted()
        {
            return _context.InformationBoard.Where(x => x.IsDeleted == true).AsEnumerable();
        }

        public async Task<InformationBoardRM> GetById(int infoId)
        {
            var result = _context.InformationBoard.Where(x => x.IsDeleted == false && x.Id == infoId)
                .Select(x => new InformationBoardRM
                {
                    Id = x.Id,
                    MessageTitle = x.MessageTitle,
                    MessageDetail = x.MessageDetail,
                    UserId = x.UserId,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy,
                    UpdatedDate = x.UpdatedDate,
                    UpdatedBy = x.UpdatedBy,
                    UserInfo = _context.Users.Where(v => v.Id == x.UserId).Select(v => new UserReadDto
                    {
                        FirstName = v.FirstName,
                        LastName = v.LastName,
                        DateOfBirth = v.DateOfBirth,
                        Department = _context.Departments.Where(vx => vx.Id == v.DepartmentId).FirstOrDefault(),
                        JobPosition = _context.JobPositions.Where(vx => vx.Id == v.JobPositionId).FirstOrDefault(),
                        EmergencyContactNumber = v.EmergencyContactNumber,
                        Gender = v.Gender,
                    }).FirstOrDefault()
                }).FirstOrDefault();
            return result;
        }

        public async Task<InformationBoard> GetInfoById(int infoId)
        {
            var result = _context.InformationBoard.Where(x => x.IsDeleted == false && x.Id == infoId)
                .Select(x => new InformationBoard
                {
                    Id = x.Id,
                    MessageTitle = x.MessageTitle,
                    MessageDetail = x.MessageDetail,
                    UserId = x.UserId,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate,
                    ApplicationUser = _context.Users.Where(v => v.Id == x.UserId).Select(v => new ApplicationUser
                    {
                        FirstName = v.FirstName,
                        LastName = v.LastName,
                        Gender = v.Gender,
                        BloodType = v.BloodType,
                        DepartmentId = v.DepartmentId,
                        JobPositionId = v.JobPositionId,
                        Department = _context.Departments.Where(vx => vx.Id == v.DepartmentId).FirstOrDefault(),
                        JobPosition = _context.JobPositions.Where(vx => vx.Id == v.JobPositionId).FirstOrDefault(),
                    }).FirstOrDefault()
                }).FirstOrDefault();
            return result;
        }

        public async Task<InformationBoard> RestoreAsync(int infoBoardId)
        {
            var result = await _context.InformationBoard.FindAsync(infoBoardId);
            result.IsDeleted = false;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(InformationBoardUpdateDto updateDto)
        {
            var infoFromDB = await _context.InformationBoard.FindAsync(updateDto.Id);

            _mapper.Map(updateDto, infoFromDB);

            await _context.SaveChangesAsync();
        }
    }
}