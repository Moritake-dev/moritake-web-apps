using System;

namespace MGAuthentication.Data.ViewModels.Location
{
    public class LocationReadVM
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
        public string CurrentLocationName { get; set; }
        public string CurrentLocationPlan { get; set; }
        public DateTime EffectiveDate { get; set; }

        public int JobPositionId { get; set; }
        public string JobPositionName { get; set; }
        public int EmployeeOrder { get; set; }
    }
}