using MGAuthentication.Data.Common;
using MGAuthentication.Data.Enums;
using System.Collections.Generic;

namespace MGAuthentication.Data.RepositoryModels
{
    public class LocationReadRM
    {
        public string UserId { get; set; }

        public string FullName
        {
            get
            {
                return string.Concat(FirstName, " ", LastName);
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; }
        public int EmployeeOrder { get; set; }
        public string JobPositionName { get; set; }

        public IList<CurrentLocationRM> CurrentLocations { get; set; }
    }
}