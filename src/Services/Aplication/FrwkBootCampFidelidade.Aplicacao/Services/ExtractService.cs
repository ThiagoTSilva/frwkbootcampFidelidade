using AutoMapper;
using FrwkBootCampFidelidade.Aplicacao.Interfaces;
using FrwkBootCampFidelidade.Dominio.ExtractContext.Entities;
using FrwkBootCampFidelidade.Dominio.ExtractContext.Interfaces;
using FrwkBootCampFidelidade.DTO.ExtractContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Aplicacao.Services
{
    public class ExtractService : IExtractService
    {
        private readonly IRansomHistoryStatusRepository _extractRepository;
        private readonly IMapper _mapper;

        public ExtractService(IRansomHistoryStatusRepository Extract, IMapper mapper)
        {
            _extractRepository = Extract;
            _mapper = mapper;
        }

        public async Task Add(RansomHistoryStatusDTO obj)
        {
            var Extract = _mapper.Map<RansomHistoryStatus>(obj);
            await _extractRepository.Add(Extract);
            await _extractRepository.SaveChanges();
        }

        public async Task<IList<ExtractDTO>> GetByUserId(int userId)
        {
            var extracts = await _extractRepository.GetByUserId(userId);

            return _mapper.Map<IList<ExtractDTO>>(extracts);
        }

        public async Task<List<RansomHistoryStatusDTO>> GetByCPF(string CPF)
        {
            var Extracts = await _extractRepository.GetByCPF(CPF);
            return _mapper.Map<List<RansomHistoryStatusDTO>>(Extracts);
        }

        public async Task<List<SummaryPointsDTO>> GetSummaryPoints(int userId)
        {
            var Extracts = await _extractRepository.GetSummaryPoints(userId);
            return _mapper.Map<List<SummaryPointsDTO>>(Extracts);
        }
    }
}
