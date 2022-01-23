using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using FrwkBootCampFidelidade.Aplicacao.Services;
using FrwkBootCampFidelidade.DTO.RansomContext;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FrwkBootCampFidelidade.RansomxUnit.Unidade
{
    [CollectionDefinition(nameof(RansomAutoMockerCollection))]
    public class RansomAutoMockerCollection : ICollectionFixture<RansomTestsAutoMockerFixture>
    { }

    public class RansomTestsAutoMockerFixture : IDisposable
    {
        public RansomService RansomService;
        public AutoMocker Mocker;

        public RansomDTO GerarRansomValido()
        {
            return GerarRansoms(1, true).FirstOrDefault();
        }

        public IEnumerable<RansomDTO> ObterRansomsVariados()
        {
            var ransoms = new List<RansomDTO>();

            ransoms.AddRange(GerarRansoms(50, true).ToList());
            ransoms.AddRange(GerarRansoms(50, true).ToList());

            return ransoms;
        }

        public IEnumerable<RansomDTO> GerarRansomItegracaoDTO(int quantidade, bool ativo)
        {
            var id = new Faker().Random.Int(1, 10000);
            var genero = new Faker().PickRandom<Name.Gender>();
            var cpf = new Faker().Person.Cpf(false);
            var amount = new Faker().Random.Long(1, 500);
            var agency = new Faker().Random.Int(5000, 5999);

            var ransomDTO = new Faker<RansomDTO>("pt_BR")
            .CustomInstantiator(f => new RansomDTO
            {
                Id = id,
                WalletId = 1,
                Amount = amount,
                Date = DateTime.Now.ToUniversalTime(),
                Beneficiary = f.Name.FullName(genero),
                Cpf = cpf,
                PixKeyType = "CPF",
                PixKey = "45387850168",
                BankNumber = "240",
                Agency = agency.ToString(),
                BankAccountNumber = "5800145",
                Operation = "Conta",
            });

            return ransomDTO.Generate(quantidade);
        }

        public IEnumerable<RansomDTO> GerarRansoms(int quantidade, bool ativo)
        {
            var id = new Faker().Random.Int(1, 10000);
            var genero = new Faker().PickRandom<Name.Gender>();
            var cpf = new Faker().Person.Cpf(false);
            var amount = new Faker().Random.Long(1, 500);
            var agency = new Faker().Random.Int(5000, 5999);

            var ransom = new Faker<RansomDTO>("pt_BR")
            .CustomInstantiator(f => new RansomDTO
            {
                Id = id,
                WalletId = 1,
                Amount = amount,
                Date = DateTime.Now.ToUniversalTime(),
                Beneficiary = f.Name.FullName(genero),
                Cpf = cpf,
                PixKeyType = "CPF",
                PixKey = "45387850168",
                BankNumber = "240",
                Agency = agency.ToString(),
                BankAccountNumber = "5800145",
                Operation = "Conta",
            });

            return ransom.Generate(quantidade);
        }


        public RansomDTO GerarRansomInvalido()
        {
            var id = 0;
            var genero = new Faker().PickRandom<Name.Gender>();
            var cpf = new Faker().Person.Cpf(false);
            var amount = new Faker().Random.Long(0, 500);
            var agency = new Faker().Random.Int(50000, 59999);

            var nome = genero.ToString();

            var ransomDTO = new Faker<RansomDTO>("pt_BR")
            .CustomInstantiator(f => new RansomDTO
            {
                Id = id,
                WalletId = 1,
                Amount = amount,
                Date = DateTime.Now.ToUniversalTime(),
                Beneficiary = f.Name.FullName(genero),
                Cpf = cpf.Replace("2", "4").Replace("0", "1"),
                PixKeyType = "CPF",
                PixKey = "45387850168",
                BankNumber = "240",
                Agency = agency.ToString(),
                BankAccountNumber = "5800145",
                Operation = "",
            });

            return ransomDTO;
        }

        public RansomService ObterRansomService()
        {
            Mocker = new AutoMocker();
            RansomService = Mocker.CreateInstance<RansomService>();
            return RansomService;
        }

        public void Dispose() { }
    }
}