using AutoMapper;
using FrwkBootCampFidelidade.Aplicacao.Services;
using FrwkBootCampFidelidade.Dominio.RansomContext.Interfaces;
using FrwkBootCampFidelidade.Dominio.RansomContext.Validator;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using UsingRansom = FrwkBootCampFidelidade.Dominio.RansomContext.Entities;

namespace FrwkBootCampFidelidade.RansomxUnit.Unidade
{
    [Collection(nameof(RansomAutoMockerCollection))]
    public class RansomServiceAutoMockerTests
    {
        readonly RansomTestsAutoMockerFixture _ransomTestsAutoMocker;
        readonly RansomService _ransomService;
        readonly IMapper _mapper;

        public RansomServiceAutoMockerTests(RansomTestsAutoMockerFixture ransomTests)
        {
            _ransomTestsAutoMocker = ransomTests;
            _ransomService = _ransomTestsAutoMocker.ObterRansomService();
            _mapper = _ransomTestsAutoMocker.Mocker.GetMock<MappingConfig>().Object.MockerMapper();
        }

        [Fact(DisplayName = "Adicionar Ransom com Sucesso")]
        [Trait("Categoria", "Ransom Service AutoMock Tests")]
        public void RansomService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var ransomDTO = _ransomTestsAutoMocker.GerarRansomValido();

            var ransom = _mapper.Map<UsingRansom.Ransom>(ransomDTO);

            _ransomTestsAutoMocker.Mocker.GetMock<IMapper>().Setup(m => m.Map<UsingRansom.Ransom>(ransomDTO))
                                    .Returns(ransom);

            // Act
            _ransomService.Add(ransomDTO).Wait();

            // Assert
            Assert.True(EhValido(ransom));
            _ransomTestsAutoMocker.Mocker.GetMock<IRansomRepository>().Verify(r => r.Add(ransom), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Ransom com Falha")]
        [Trait("Categoria", "Ransom Service AutoMock Tests")]
        public void RansomService_Adicionar_DeveFalharDevidoRansomInvalido()
        {
            // Arrange
            var ransomDTO = _ransomTestsAutoMocker.GerarRansomInvalido();

            var ransom = _mapper.Map<UsingRansom.Ransom>(ransomDTO);

            _ransomTestsAutoMocker.Mocker.GetMock<IMapper>().Setup(m => m.Map<UsingRansom.Ransom>(ransomDTO))
                                    .Returns(ransom);

            // Act
            _ransomService.Add(ransomDTO).Wait();

            // Assert
            Assert.False(EhValido(ransom));
            _ransomTestsAutoMocker.Mocker.GetMock<IRansomRepository>().Verify(r => r.Add(ransom), Times.Never);
        }

        [Fact(DisplayName = "Obter Todos Ransoms")]
        [Trait("Categoria", "Ransom Service AutoMock Tests")]
        public void RansomService_ObterTodos_DeveRetornarTodosRansoms()
        {
            // Arrange

            // Act
            var ransomsDTO = _ransomTestsAutoMocker.ObterRansomsVariados();

            var ransoms = _mapper.Map<IEnumerable<UsingRansom.Ransom>>(ransomsDTO);

            _ransomTestsAutoMocker.Mocker.GetMock<IMapper>().Setup(m =>
                m.Map<IEnumerable<UsingRansom.Ransom>>(ransomsDTO)).Returns(ransoms);

            _ransomTestsAutoMocker.Mocker.GetMock<IRansomRepository>().Setup(r => r.GetAll(true))
                .Returns(ransoms);

            // Assert 
            Assert.True(ransoms.Any());
            _ransomTestsAutoMocker.Mocker.GetMock<IRansomRepository>().Verify(r => r.GetAll(true), Times.AtMostOnce);
        }

        private bool EhValido(UsingRansom.Ransom ransom)
        {
            return new RansomValidator().Validate(ransom).IsValid;
        }

    }
}