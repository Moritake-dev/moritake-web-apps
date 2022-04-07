using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGAuthentication.Data.ViewModels.Location
{
    public class LocationEditVM
    {
        public int Id { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string CurrentLocationName { get; set; }
        public string CurrentLocationPlan { get; set; }
    }
}