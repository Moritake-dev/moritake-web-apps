﻿using MGAuthentication.Data.User;
using System.Collections.Generic;

namespace MGAuthentication.Data.Common
{
    public class JobPosition : AuditableEntity<int>
    {
        public string Name { get; set; }
        public ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}