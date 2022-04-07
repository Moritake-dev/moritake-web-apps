using MGAuthentication.Data.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGAuthentication.Data.RepositoryModels
{
    public class InformationBoardRM
    {
        public int Id { get; set; }
        public string MessageTitle { get; set; }
        public string MessageDetail { get; set; }

        // USER RELATED INFO
        public string UserId { get; set; }

        public UserReadDto UserInfo { get; set; }

        // USER RELATED INFO
#nullable enable
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
#nullable disable
    }
}