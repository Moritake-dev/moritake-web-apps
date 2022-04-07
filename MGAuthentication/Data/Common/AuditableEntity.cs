using System;

namespace MGAuthentication.Data.Common
{
    public class AuditableEntity<T> : Entity<T>
    {
#nullable enable
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
#nullable disable

        /// <summary>
        ///     Soft deleting properties
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}