using MGAuthentication.Data;
using MGAuthentication.Data.Common;
using MGAuthentication.Data.User;
using MGAuthentication.Data.ViewModels.User;
using MGAuthentication.Data.ViewModels.UserProfile;
using MGAuthentication.Respositories.UserRepositories;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace MGAuthentication.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;

        public UserService(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _context = context;
        }

        public bool DoesExist(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).GetAwaiter().GetResult();
            if (user == null)
                return false;
            return true;
        }

        public IEnumerable<UserReadVM> GetAllUserInfo()
        {
            var result = _context.Users.Select(x => new UserReadVM
            {
                UserId = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Gender = x.Gender,
                BloodType = x.BloodType,
                RestType = x.RestType,
                PostNo = x.Address.PostNo,
                PermanentAddress = x.Address.Detail,
                //CurrentLocation = x.CurrentLocation,
                PhoneNo = x.PhoneNumber,
                Email = x.Email,
                DateOfBirth = x.DateOfBirth,
                EmergencyContactNumber = x.EmergencyContactNumber,
            }).ToList();

            return result;
            //var result = new List<UserReadVM>();
            //var resultModel = _userRepository.GetAllUserInfo().ToList();
        }

        public UserReadVM GetUserInfoById(string id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).GetAwaiter().GetResult();
            var userRoles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();
            var userClaims = _userManager.GetClaimsAsync(user).GetAwaiter().GetResult();

            var result = new UserReadVM
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                BloodType = user.BloodType,
                RestType = user.RestType,
                PostNo = user.Address.PostNo,
                PermanentAddress = user.Address.Detail,
                //CurrentLocation = user.CurrentLocation,
                PhoneNo = user.PhoneNumber,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                EmergencyContactNumber = user.EmergencyContactNumber,
                DepartmentId = user.DepartmentId,
                DepartmentName = _context.Departments.Where(x => x.Id == user.DepartmentId).Select(x => x.Name).FirstOrDefault().ToString(),
                //DepartmentName = user.Department.Name,
                JobPositionId = user.JobPositionId,
                JobPositionName = _context.JobPositions.Where(x => x.Id == user.JobPositionId).Select(x => x.Name).FirstOrDefault().ToString(),
                //JobPositionName = user.JobPosition.Name,
                //DepartmentId = user.Department.Id,
                //DepartmentName = user.Department.Name,
                //JobPositionId = user.JobPosition.Id,
                //JobPositionName = user.JobPosition.Name,

                Roles = userRoles,
                Claims = userClaims
            };

            return result;
        }

        public void ProfileUpdate(string id, ProfileUpdateVM updateDto)
        {
            var userModel = _context.Users.Where(x => x.Id == id).FirstOrDefault();

            userModel.FirstName = updateDto.FirstName;
            userModel.LastName = updateDto.LastName;
            userModel.EmergencyContactNumber = updateDto.EmergencyContactNumber;
            userModel.DateOfBirth = updateDto.DateOfBirth;
            userModel.Address = new Address(updateDto.PostNo, updateDto.PermanentAddress);
            userModel.DepartmentId = updateDto.DepartmentId;
            userModel.JobPositionId = updateDto.JobPositionId;

            if (updateDto.PhoneNo != null)
            {
                userModel.PhoneNumber = updateDto.PhoneNo;
            }

            if (updateDto.Email != null)
            {
                userModel.Email = updateDto.Email;
            }

            _context.SaveChanges();
        }
    }
}