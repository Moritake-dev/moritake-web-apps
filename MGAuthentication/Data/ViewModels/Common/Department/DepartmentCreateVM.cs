using System;

namespace MGAuthentication.Data.ViewModels.Common.Department
{
    public class DepartmentCreateVM
    {
        public string Name { get; set; }
#nullable enable
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
#nullable disable
    }
}