using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGAuthentication.Data.DTOs.InformationBoardDTOs
{
    public class InformationBoardCreateDto
    {
        public string MessageTitle { get; set; }
        public string MessageDetail { get; set; }
        public string UserId { get; set; }
    }
}