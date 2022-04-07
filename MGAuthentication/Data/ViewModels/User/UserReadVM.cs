using MGAuthentication.Data.Enums;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace MGAuthentication.Data.ViewModels.User
{
    public class UserReadVM
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return string.Concat(FirstName, " ", LastName);
            }
        }

        public string JobTitle { get; set; }
        public Gender Gender { get; set; }
        public BloodType BloodType { get; set; }
        public RestType RestType { get; set; }
        public string PostNo { get; set; }
        public string PermanentAddress { get; set; }
        public string CurrentLocation { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EmergencyContactNumber { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public int JobPositionId { get; set; }
        public string JobPositionName { get; set; }

        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}