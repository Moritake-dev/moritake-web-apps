using MGAuthentication.Data.Common;
using MGAuthentication.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MGAuthentication.Data.User
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            UserCurrentLocations = new List<UserCurrentLocation>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return String.Concat(FirstName, " ", LastName);
            }
        }

        public Gender Gender { get; set; }
        public BloodType BloodType { get; set; }
        public RestType RestType { get; set; }
        public Address Address { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string EmergencyContactNumber { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsEmployeeProfile { get; set; }
        public bool IsActiveEmployee { get; set; }
        public int EmployeeOrder { get; set; }

        // For Joining other tables
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public int JobPositionId { get; set; }
        public JobPosition JobPosition { get; set; }
        public ICollection<UserCurrentLocation> UserCurrentLocations { get; private set; }

        public ICollection<InformationBoard> InformationBoard { get; set; }
    }
}