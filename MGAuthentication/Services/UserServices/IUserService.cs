using MGAuthentication.Data.ViewModels.User;
using MGAuthentication.Data.ViewModels.UserProfile;
using System.Collections.Generic;

namespace MGAuthentication.Services
{
    /// <summary>
    ///     Returns appliation specific user related services
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        ///     Returns all the users in the database.
        /// </summary>
        /// <returns>List of UserReadVM</returns>
        IEnumerable<UserReadVM> GetAllUserInfo();

        /// <summary>
        ///     Returns all information about the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserReadVM GetUserInfoById(string id);

        /// <summary>
        ///     Updates user profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDto"></param>
        void ProfileUpdate(string id, ProfileUpdateVM updateDto);

        /// <summary>
        ///     Checks if the user exists or not
        /// </summary>
        /// <param name="userId">Current UserId</param>
        /// <returns>Bool</returns>
        bool DoesExist(string userId);
    }
}