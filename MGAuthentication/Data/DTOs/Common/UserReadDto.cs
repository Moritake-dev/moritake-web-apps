using MGAuthentication.Data.Common;
using MGAuthentication.Data.Enums;
using System;

namespace MGAuthentication.Data.DTOs.Common
{
    public class UserReadDto
    {
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
        public Address Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EmergencyContactNumber { get; set; }
        public Department Department { get; set; }
        public JobPosition JobPosition { get; set; }
    }
}