using AutoMapper;
using FluentAssertions;
using FrwkBootCampFidelidade.Aplicacao.Services;
using FrwkBootCampFidelidade.Dominio.PromotionContext.Interfaces;
using FrwkBootCampFidelidade.DTO.PromotionContext;
using FrwkBootCampFidelidade.Infraestrutura.IOC.IOC;
using FrwkBootCampFidelidade.Promotions.FakeData.PromotionData;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FrwkBootCampFidelidade.Promotion.Tests.Service
{
    public class PromotionServiceTest
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IMapper _mapper;

        private readonly PromotionService _promotionService;

        private readonly Dominio.PromotionContext.Entities.Promotion _promotion;

        public PromotionServiceTest()
        {
            _promotionRepository = Substitute.For<IPromotionRepository>();
            _mapper = new MapperConfiguration(p => p.AddProfile<AutoMapping>()).CreateMapper();

            _promotionService = new PromotionService(_mapper, _promotionRepository);

            _promotion = new PromotionFaker().Generate();
        }

        [Fact]
        public async Task GetPromotionByDateRange_NotEmpty()
        {
            var list = new PromotionFaker().Generate(10);
            var control = _mapper.Map<IEnumerable<PromotionDTO>>(list);
            _promotionRepository.GetPromotionByDateRange(Arg.Any<Dominio.PromotionContext.Entities.Promotion>()).Returns(list);

            var result = await _promotionService.GetPromotionByDateRange(new PromotionDTO());

            await _promotionRepository.Received().GetPromotionByDateRange(Arg.Any<Dominio.PromotionContext.Entities.Promotion>());
            result.Should().BeEquivalentTo(control);
        }

        [Fact]
        public async Task GetPromotionByDateRange_Empty()
        {
            var list = new List<Dominio.PromotionContext.Entities.Promotion>();
            var control = _mapper.Map<IEnumerable<PromotionDTO>>(list);
            _promotionRepository.GetPromotionByDateRange(Arg.Any<Dominio.PromotionContext.Entities.Promotion>()).Returns(list);

            var result = await _promotionService.GetPromotionByDateRange(new PromotionDTO());

            await _promotionRepository.Received().GetPromotionByDateRange(Arg.Any<Dominio.PromotionContext.Entities.Promotion>());
            result.Should().BeEquivalentTo(control);
        }

        [Fact]
        public async Task GetPromotionToday_NotEmpty()
        {
            var list = new PromotionFaker().Generate(10);
            var control = _mapper.Map<IEnumerable<PromotionDTO>>(list);
            _promotionRepository.GetPromotionToday(Arg.Any<Dominio.PromotionContext.Entities.Promotion>()).Returns(list);

            var result = await _promotionService.GetPromotionToday(new PromotionDTO());

            await _promotionRepository.Received().GetPromotionToday(Arg.Any<Dominio.PromotionContext.Entities.Promotion>());
            result.Should().BeEquivalentTo(control);
        }

        [Fact]
        public async Task GetPromotionToday_Empty()
        {
            var list = new List<Dominio.PromotionContext.Entities.Promotion>();
            var control = _mapper.Map<IEnumerable<PromotionDTO>>(list);
            _promotionRepository.GetAll().Returns(list);

            var result = await _promotionService.GetAll();

            await _promotionRepository.Received().GetAll();
            result.Should().BeEquivalentTo(control);
        }

        [Fact]
        public async Task GetAll_NotEmpty()
        {
            var list = new PromotionFaker().Generate(10);
            var control = _mapper.Map<IEnumerable<PromotionDTO>>(list);
            _promotionRepository.GetAll().Returns(list);

            var result = await _promotionService.GetAll();

            await _promotionRepository.Received().GetAll();
            result.Should().BeEquivalentTo(control);
        }

        [Fact]
        public async Task GetAll_Empty()
        {
            var list = new List<Dominio.PromotionContext.Entities.Promotion>();
            var control = _mapper.Map<IEnumerable<PromotionDTO>>(list);
            _promotionRepository.GetAll().Returns(list);

            var result = await _promotionService.GetAll();

            await _promotionRepository.Received().GetAll();
            result.Should().BeEquivalentTo(control);
        }

        [Fact]
        public async Task GetById_Null()
        {
            _promotionRepository.GetById(Arg.Any<string>()).ReturnsNull();

            var result = await _promotionService.GetById("");

            await _promotionRepository.Received().GetById(Arg.Any<string>());
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetById_NotNull()
        {
            var control = _mapper.Map<PromotionDTO>(_promotion);
            _promotionRepository.GetById(Arg.Any<string>()).Returns(_promotion);

            var result = await _promotionService.GetById("");

            await _promotionRepository.Received().GetById(Arg.Any<string>());
            result.Should().BeEquivalentTo(control);
        }

        [Fact]
        public async Task Add_Success()
        {
            var control = _mapper.Map<PromotionDTO>(_promotion);
            _promotionRepository.Add(Arg.Any<Dominio.PromotionContext.Entities.Promotion>()).Returns(_promotion);

            var result = await _promotionService.Add(new PromotionCreateUpdateRemoveDTO());

            await _promotionRepository.Received().Add(Arg.Any<Dominio.PromotionContext.Entities.Promotion>());
            result.Should().BeEquivalentTo(control);
        }

        [Fact]
        public async Task Update_True()
        {
            _promotionRepository.GetById(Arg.Any<string>()).Returns(_promotion);
            _promotionRepository.Update(Arg.Any<Dominio.PromotionContext.Entities.Promotion>()).Returns(true);

            var result = await _promotionService.Update(new PromotionCreateUpdateRemoveDTO());

            await _promotionRepository.Received().GetById(Arg.Any<string>());
            await _promotionRepository.Received().Update(Arg.Any<Dominio.PromotionContext.Entities.Promotion>());
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Update_False()
        {
            _promotionRepository.GetById(Arg.Any<string>()).ReturnsNull();
            _promotionRepository.Update(Arg.Any<Dominio.PromotionContext.Entities.Promotion>()).Returns(false);

            var result = await _promotionService.Update(new PromotionCreateUpdateRemoveDTO());

            await _promotionRepository.Received().GetById(Arg.Any<string>());
            result.Should().BeFalse();
        }

        [Fact]
        public async Task Remove_True()
        {
            _promotionRepository.Remove(Arg.Any<Dominio.PromotionContext.Entities.Promotion>()).Returns(true);

            var result = await _promotionService.Remove(new PromotionCreateUpdateRemoveDTO());

            await _promotionRepository.Received().Remove(Arg.Any<Dominio.PromotionContext.Entities.Promotion>());
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Remove_False()
        {
            _promotionRepository.Remove(Arg.Any<Dominio.PromotionContext.Entities.Promotion>()).Returns(false);

            var result = await _promotionService.Remove(new PromotionCreateUpdateRemoveDTO());

            await _promotionRepository.Received().Remove(Arg.Any<Dominio.PromotionContext.Entities.Promotion>());
            result.Should().BeFalse();
        }

        [Fact]
        public async Task RemoveById_True()
        {
            _promotionRepository.RemoveById(Arg.Any<string>()).Returns(true);

            var result = await _promotionService.RemoveById("");

            await _promotionRepository.Received().RemoveById(Arg.Any<string>());
            result.Should().BeTrue();
        }

        [Fact]
        public async Task RemoveById_False()
        {
            _promotionRepository.RemoveById(Arg.Any<string>()).Returns(false);

            var result = await _promotionService.RemoveById("");

            await _promotionRepository.Received().RemoveById(Arg.Any<string>());
            result.Should().BeFalse();
        }
    }
}
