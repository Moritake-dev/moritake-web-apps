using MGAuthentication.Data.DTOs.Common;
using MGAuthentication.Data.User;
using System;

namespace MGAuthentication.Data.DTOs.InformationBoardDTOs
{
    public class InformationBoardReadDto
    {
        public int Id { get; set; }

        // MESSAGE RELATED
        public string MessageTitle { get; set; }

        public string MessageDetail { get; set; }
        // MESSAGE RELATED
#nullable enable
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
#nullable disable
        public bool IsDeleted { get; set; }

        // User Information
        public string UserId { get; set; }

        public UserReadDto UserInfo { get; set; }
    }
}