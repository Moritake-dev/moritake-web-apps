namespace MGAuthentication.Data.User
{
    public class UserCurrentLocation
    {
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int CurrentLocationId { get; set; }
        public CurrentLocation CurrentLocation { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovedBy { get; set; }
    }
}