using MGAuthentication.Data.User;
using MGAuthentication.Data.ViewModels.UserProfile;
using System.Collections.Generic;

namespace MGAuthentication.Respositories.UserRepositories
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetAllUserInfo();

        ApplicationUser GetUserInfoById(string id);

        void UserProfileUpdate(string id, ProfileUpdateVM updateDto);

        bool DoesExist(string userId);
    }
}