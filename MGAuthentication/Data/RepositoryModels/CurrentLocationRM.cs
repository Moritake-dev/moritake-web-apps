using System;

namespace MGAuthentication.Data.RepositoryModels
{
    public class CurrentLocationRM
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string LocationPlan { get; set; }
        public DateTime EffectiveDate { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovedBy { get; set; }
    }
}