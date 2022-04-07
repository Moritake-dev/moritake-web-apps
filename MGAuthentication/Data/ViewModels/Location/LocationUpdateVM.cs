using System;

namespace MGAuthentication.Data.ViewModels.Location
{
    public class LocationUpdateVM
    {
        public string UserId { get; set; }
        public int LocationId { get; set; }
        public string FullName { get; set; }
        public string CurrentLocationName { get; set; }
        public string CurrentLocationPlan { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}