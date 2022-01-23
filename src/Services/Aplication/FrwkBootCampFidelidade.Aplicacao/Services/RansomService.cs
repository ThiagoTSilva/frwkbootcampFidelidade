using AutoMapper;
using FrwkBootCampFidelidade.Aplicacao.Interfaces;
using FrwkBootCampFidelidade.Dominio.RansomContext.Entities;
using FrwkBootCampFidelidade.Dominio.RansomContext.Interfaces;
using FrwkBootCampFidelidade.Dominio.RansomContext.Validator;
using FrwkBootCampFidelidade.DTO.RansomContext;
using FrwkBootCampFidelidade.DTO.WalletContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrwkBootCampFidelidade.Aplicacao.Services
{
    public class RansomService : IRansomService
    {
        private readonly IRansomRepository _ransomRepository;
        private readonly IWalletService _walletService;
        private readonly IMapper _mapper;

        public RansomService(IRansomRepository ransomRepository, IWalletService walletService, IMapper mapper)
        {
            _ransomRepository = ransomRepository;
            _walletService = walletService;
            _mapper = mapper;
        }

        public async Task<RansomDTO> Add(RansomDTO ransomDTO)
        {
            if (ransomDTO == null) return null;

            var ransom = _mapper.Map<Ransom>(ransomDTO);

            if (!EhValido(ransom)) return null;

            ransom.Created = DateTime.Now;
            ransom.Updated = DateTime.Now;

            try
            {
                await _walletService.Withdraw(new WalletWithdrawDTO { WalletId = (int)ransomDTO.WalletId, Amount = ransomDTO.Amount });

                await _ransomRepository.Add(ransom);
            }
            catch
            {
            }

            return null;
        }

        public async Task Remove(int id)
        {
            Ransom ransom = await _ransomRepository.GetById(id);

            if (ransom != null)
            {
                try
                {
                    _ransomRepository.Remove(ransom);
                    await _ransomRepository.SaveChanges();
                }
                catch
                {

                }
            }
        }

        public IEnumerable<RansomDTO> GetAll()
        {
            var ransoms = _ransomRepository.GetAll();

            try
            {
                var ransomDTOs = _mapper.Map<IEnumerable<RansomDTO>>(ransoms);

                return ransomDTOs;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<RansomDTO>> GetListByCPF(string cpf)
        {
            List<RansomDTO> ransomDTOs = _mapper.Map<List<RansomDTO>>(await _ransomRepository.GetListByCPF(cpf));
            return ransomDTOs;
        }

        public Task<RansomDTO> GetById(int Id)
        {
            throw new System.NotImplementedException();
        }

        private bool EhValido(Ransom ransom)
        {
            return new RansomValidator().Validate(ransom).IsValid;
        }

    }
}
