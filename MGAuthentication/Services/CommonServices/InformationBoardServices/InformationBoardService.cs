using AutoMapper;
using MGAuthentication.Data.Common;
using MGAuthentication.Data.DTOs.InformationBoardDTOs;
using MGAuthentication.Respositories.Common.InformationBoardRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGAuthentication.Services.CommonServices.InformationBoardServices
{
    public class InformationBoardService : IInformationBoardService
    {
        private readonly IInformationBoardRepository _repository;
        private readonly IMapper _mapper;

        public InformationBoardService(IInformationBoardRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InformationBoardReadDto> Create(InformationBoardCreateDto infoBoardDto)
        {
            var model = _mapper.Map<InformationBoard>(infoBoardDto);
            await _repository.Create(model);
            await _repository.SaveChangesAsync();
            return (_mapper.Map<InformationBoardReadDto>(model));
        }

        public async Task Delete(int infoId)
        {
            await _repository.Delete(infoId);
        }

        public IEnumerable<InformationBoardReadDto> GetAll()
        {
            var result = _repository.GetAll().ToList();
            // TODO MAP ALL CORRECT INFO
            return _mapper.Map<IEnumerable<InformationBoardReadDto>>(result);
        }

        public IEnumerable<InformationBoardReadDto> GetAllDeleted()
        {
            var result = _repository.GetAllDeleted().ToList();
            return _mapper.Map<IEnumerable<InformationBoardReadDto>>(result);
        }

        public async Task<InformationBoardReadDto> GetInfoById(int infoId)
        {
            var result = await _repository.GetInfoById(infoId);
            return _mapper.Map<InformationBoardReadDto>(result);
        }

        public async Task<InformationBoardReadDto> RestoreAsync(int infoId)
        {
            var restoredModel = await _repository.RestoreAsync(infoId);
            var result = _mapper.Map<InformationBoardReadDto>(restoredModel);
            return result;
        }

        public async Task UpdateAsync(InformationBoardUpdateDto updateDto)
        {
            await _repository.Update(updateDto);
        }
    }
}