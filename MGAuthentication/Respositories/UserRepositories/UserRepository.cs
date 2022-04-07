using MGAuthentication.Data;
using MGAuthentication.Data.Common;
using MGAuthentication.Data.User;
using MGAuthentication.Data.ViewModels.UserProfile;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace MGAuthentication.Respositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserRepository(UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public bool DoesExist(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).GetAwaiter().GetResult();
            if (user == null)
                return false;
            return true;
        }

        public IEnumerable<ApplicationUser> GetAllUserInfo()
        {
            var result = _context.Users.AsEnumerable();
            return result;
        }

        public ApplicationUser GetUserInfoById(string id)
        {
            var result = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            return result;
        }

        public void UserProfileUpdate(string id, ProfileUpdateVM updateVm)
        {
            var userModel = _context.Users.Where(x => x.Id == id).FirstOrDefault();

            userModel.FirstName = updateVm.FirstName;
            userModel.LastName = updateVm.LastName;
            userModel.EmergencyContactNumber = updateVm.EmergencyContactNumber;
            userModel.DateOfBirth = updateVm.DateOfBirth;
            userModel.Address = new Address(updateVm.PostNo, updateVm.PermanentAddress);
            userModel.DepartmentId = updateVm.DepartmentId;
            userModel.JobPositionId = updateVm.JobPositionId;

            if (updateVm.PhoneNo != null)
            {
                userModel.PhoneNumber = updateVm.PhoneNo;
            }

            if (updateVm.Email != null)
            {
                userModel.Email = updateVm.Email;
            }

            _context.SaveChanges();
        }
    }
}