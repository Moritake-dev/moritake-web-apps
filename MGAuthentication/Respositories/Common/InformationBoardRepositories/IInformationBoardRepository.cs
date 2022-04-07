using MGAuthentication.Data.Common;
using MGAuthentication.Data.DTOs.InformationBoardDTOs;
using MGAuthentication.Data.RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGAuthentication.Respositories.Common.InformationBoardRepositories
{
    public interface IInformationBoardRepository
    {
        IEnumerable<InformationBoardRM> GetAll();

        IEnumerable<InformationBoard> GetAllDeleted();

        Task<InformationBoardRM> GetById(int infoId);

        Task<InformationBoard> GetInfoById(int infoId);

        Task Create(InformationBoard informationBoard);

        Task Update(InformationBoardUpdateDto updateDto);

        Task Delete(int infoId);

        Task<InformationBoard> RestoreAsync(int infoBoardId);

        Task SaveChangesAsync();
    }
}