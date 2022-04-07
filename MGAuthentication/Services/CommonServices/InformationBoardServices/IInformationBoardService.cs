using MGAuthentication.Data.DTOs.InformationBoardDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGAuthentication.Services.CommonServices.InformationBoardServices
{
    public interface IInformationBoardService
    {
        IEnumerable<InformationBoardReadDto> GetAll();

        IEnumerable<InformationBoardReadDto> GetAllDeleted();

        Task<InformationBoardReadDto> GetInfoById(int infoId);

        Task<InformationBoardReadDto> Create(InformationBoardCreateDto infoBoardDto);

        Task UpdateAsync(InformationBoardUpdateDto updateDto);

        Task<InformationBoardReadDto> RestoreAsync(int infoId);

        Task Delete(int infoId);
    }
}