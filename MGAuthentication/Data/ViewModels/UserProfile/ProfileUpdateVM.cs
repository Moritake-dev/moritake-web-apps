using System;
using System.ComponentModel.DataAnnotations;

namespace MGAuthentication.Data.ViewModels.UserProfile
{
    public class ProfileUpdateVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string JobTitle { get; set; }

        [Required]
        public string PermanentAddress { get; set; }

        [Required]
        public string PostNo { get; set; }

        public int DepartmentId { get; set; }
        public int JobPositionId { get; set; }
    }
}