using MGAuthentication.Data.Common;
using System;
using System.Collections.Generic;

namespace MGAuthentication.Data.User
{
    public class CurrentLocation : AuditableEntity<int>
    {
        public CurrentLocation()
        {
            UserCurrentLocations = new List<UserCurrentLocation>();
        }

        public string CurrentLocationName { get; set; }
        public string CurrentLocationPlan { get; set; }
        public DateTime EffectiveDate { get; set; }
        public ICollection<UserCurrentLocation> UserCurrentLocations { get; private set; }
    }
}