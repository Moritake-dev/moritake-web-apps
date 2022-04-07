using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGAuthentication.Data.DTOs.InformationBoardDTOs
{
    public class InformationBoardUpdateDto
    {
        public int Id { get; set; }
        public string MessageTitle { get; set; }
        public string MessageDetail { get; set; }
    }
}