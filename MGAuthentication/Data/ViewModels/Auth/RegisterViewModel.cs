using MGAuthentication.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace MGAuthentication.Data.ViewModels.Auth
{
    public class RegisterViewModel
    {
        public string ReturnUrl { get; set; }

        [Required]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public BloodType BloodType { get; set; }
        public RestType RestType { get; set; }

        [Required]
        public string PostNo { get; set; }

        [Required]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string EmergencyContactNumber { get; set; }

        public string CurrentLocation { get; set; }
        public string JobTitle { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        public bool IsEmployeeProfile { get; set; }

        [Required]
        public bool IsActiveEmployee { get; set; }

        // For Joining other tables
        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int JobPositionId { get; set; }

        [Required]
        public int EmployeeOrder { get; set; }
    }
}