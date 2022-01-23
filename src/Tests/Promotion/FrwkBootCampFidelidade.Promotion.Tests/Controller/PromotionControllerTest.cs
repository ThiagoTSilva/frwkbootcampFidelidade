using FluentAssertions;
using FrwkBootCampFidelidade.Aplicacao.Interfaces;
using FrwkBootCampFidelidade.DTO.PromotionContext;
using FrwkBootCampFidelidade.Promotion.API.Controllers;
using FrwkBootCampFidelidade.Promotion.FakeData.PromotionData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FrwkBootCampFidelidade.Promotion.Tests.Controller
{
    public class PromotionControllerTest
    {
        private readonly IPromotionService _promotionService;
        private readonly PromotionController _controller;
        private readonly PromotionDTO _promotionDTO;
        private readonly List<PromotionDTO> _listPromotionDTO;
        public PromotionControllerTest()
        {
            _promotionService = Substitute.For<IPromotionService>();
            _controller = new PromotionController(_promotionService);

            _promotionDTO = new PromotionDTOFaker().Generate();
            _listPromotionDTO = new PromotionDTOFaker().Generate(2);
        }

        [Fact]
        public async Task GetAll_Ok()
        {
            var control = new List<PromotionDTO>();
            _listPromotionDTO.ToList().ForEach(x => control.Add(x.CloneTyped()));
            _promotionService.GetAll().Returns(_listPromotionDTO);

            var result = (ObjectResult)await _controller.GetAll();

            await _promotionService.Received().GetAll();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(control);
        }

        [Fact]
        public async Task GetById_Ok()
        {
            _promotionService.GetById(Arg.Any<string>()).Returns(_promotionDTO.CloneTyped());

            var result = (ObjectResult)await _controller.GetById(_promotionDTO.Id);

            await _promotionService.Received().GetById(Arg.Any<string>());
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(_promotionDTO);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            _promotionService.GetById(Arg.Any<string>()).ReturnsNull();

            var result = (StatusCodeResult)await _controller.GetById("");

            await _promotionService.Received().GetById(Arg.Any<string>());
            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetPromotionByDateRange_Ok()
        {
            var control = new List<PromotionDTO>();
            _listPromotionDTO.ToList().ForEach(x => control.Add(x.CloneTyped()));
            _promotionService.GetPromotionByDateRange(Arg.Any<PromotionDTO>()).Returns(_listPromotionDTO);

            var result = (ObjectResult)await _controller.GetPromotionByDateRange(1, 1, DateTime.Now, DateTime.Now);

            await _promotionService.Received().GetPromotionByDateRange(Arg.Any<PromotionDTO>());
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(control);
        }

        [Fact]
        public async Task GetPromotionToday_Ok()
        {
            var control = new List<PromotionDTO>();
            _listPromotionDTO.ToList().ForEach(x => control.Add(x.CloneTyped()));
            _promotionService.GetPromotionToday(Arg.Any<PromotionDTO>()).Returns(_listPromotionDTO);

            var result = (ObjectResult)await _controller.GetPromotionToday(1, 1);

            await _promotionService.Received().GetPromotionToday(Arg.Any<PromotionDTO>());
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(control);
        }

        [Fact]
        public async Task Add_Created()
        {
            _promotionService.Add(Arg.Any<PromotionCreateUpdateRemoveDTO>()).Returns(_promotionDTO.CloneTyped());

            var result = (ObjectResult)await _controller.Add(new PromotionCreateUpdateRemoveDTO());

            await _promotionService.Received().Add(Arg.Any<PromotionCreateUpdateRemoveDTO>());
            result.StatusCode.Should().Be(StatusCodes.Status201Created);
            result.Value.Should().BeEquivalentTo(_promotionDTO);
        }

        [Fact]
        public async Task Update_Ok()
        {
            _promotionService.Update(Arg.Any<PromotionCreateUpdateRemoveDTO>()).Returns(true);

            var result = (ObjectResult)await _controller.Update(new PromotionCreateUpdateRemoveDTO());

            await _promotionService.Received().Update(Arg.Any<PromotionCreateUpdateRemoveDTO>());
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(true);
        }

        [Fact]
        public async Task Update_BedRequest()
        {
            var result = (StatusCodeResult)await _controller.Update(null);

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task Remove_Ok()
        {
            _promotionService.Remove(Arg.Any<PromotionCreateUpdateRemoveDTO>()).Returns(true);

            var result = (ObjectResult)await _controller.Remove(new PromotionCreateUpdateRemoveDTO());

            await _promotionService.Received().Remove(Arg.Any<PromotionCreateUpdateRemoveDTO>());
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(true);
        }

        [Fact]
        public async Task Remove_BedRequest()
        {
            PromotionCreateUpdateRemoveDTO promotionDTO_null = null;
            var result = (StatusCodeResult)await _controller.Remove(promotionDTO_null);

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task RemoveById_Ok()
        {
            _promotionService.RemoveById(Arg.Any<string>()).Returns(true);

            var result = (ObjectResult)await _controller.Remove("");

            await _promotionService.Received().RemoveById(Arg.Any<string>());
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(true);
        }
    }
}
