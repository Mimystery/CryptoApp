using AutoMapper;
using CryptoApp.API.Controllers;
using CryptoApp.Application.Services;
using CryptoApp.Core.Contracts;
using CryptoApp.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Tests.Controllers
{
    public class TelegramControllerTests
    {
        [Fact]
        public async Task Authenticate_ShouldReturnToken()
        {
            //Arrange
            var mockService = new Mock<ITelegramUserService>();
            var mockMapper = new Mock<IMapper>();

            var request = new TelegramAuthResponse();
            var mappeduser = new TelegramUser();
            var expectedToken = "mocked_token";

            mockService.Setup(s => s.VerifyTelegramAuth(request)).Returns(true);
            mockMapper.Setup(m => m.Map<TelegramUser>(request)).Returns(mappeduser);
            mockService.Setup(s => s.LoginTelegramUser(mappeduser)).ReturnsAsync(expectedToken);

            var controller = new TelegramController(mockService.Object, mockMapper.Object);
            //Act
            var result = await controller.Authenticate(request);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var tokenProperty = okResult.Value!.GetType().GetProperty("token");
            string? token = tokenProperty!.GetValue(okResult.Value) as string;

            Assert.NotNull(token);
            Assert.Equal(expectedToken, token);
        }

        [Fact]
        public async Task Authenticate_ShouldReturnUnauthorized()
        {
            //Arange
            var mockService = new Mock<ITelegramUserService>();

            var request = new TelegramAuthResponse();

            mockService.Setup(s => s.VerifyTelegramAuth(request)).Returns(false);

            var controller = new TelegramController(mockService.Object, null);

            //Act
            var result = await controller.Authenticate(request);

            //Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result.Result);
            Assert.NotNull(unauthorizedResult);
            Assert.Equal("Invalid data", unauthorizedResult.Value);
        }

        [Fact]
        public async Task GetTelegramUserById_ShouldReturnUser()
        {
            var mockService = new Mock<ITelegramUserService>();

            int requestId = 12345;
            var expectedUser = new TelegramUser
            {
                Id = requestId,
                AuthDate = 20231010,
                FirstName = null,
                LastName = null,
                Hash = "mocked_hash",
                Username = "mocked_username",
                PhotoUrl = null,
                Transactions = new List<CoinTransaction>()
            };

            mockService.Setup(s => s.GetTelegramUserById(requestId)).ReturnsAsync(expectedUser);

            var controller = new TelegramController(mockService.Object, null);

            //Act
            var result = await controller.GetTelegramUser(requestId);
            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var user = Assert.IsType<TelegramUser>(okResult.Value);

            Assert.NotNull(user);
            Assert.Equal(expectedUser.Id, user.Id);
        }
        [Fact]
        public async Task GetTelegramUserById_ShoudReturnNotFound()
        {
            var mockService = new Mock<ITelegramUserService>();
            int requestId = 12345;

            var expectedUser = (TelegramUser?)null;

            mockService.Setup(s => s.GetTelegramUserById(requestId)).ReturnsAsync(expectedUser);

            var controller = new TelegramController(mockService.Object, null);

            //Act
            var result = await controller.GetTelegramUser(requestId);

            //Assert
            var notFoundresult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.NotNull(notFoundresult);
            Assert.Equal("Telegram user not found", notFoundresult.Value);
        }

        [Fact]
        public async Task AddTransaction_ShouldReturnOk()
        {
            //Arrange
            var mockService = new Mock<ITelegramUserService>();
            int requestId = 12345;
            var coinTransaction = new CoinTransactionRequest();

            mockService.Setup(s => s.AddTransaction(requestId, coinTransaction)).Returns(Task.CompletedTask);

            var controller = new TelegramController(mockService.Object, null);

            //Act
            var result = await controller.AddTransaction(requestId, coinTransaction);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Transaction added successfully", okResult.Value);
        }

        [Fact]
        public async Task AddTransaction_ShouldReturnBadRequest()
        {
            //Arrange
            var mockService = new Mock<ITelegramUserService>();
            int requestId = 12345;

            var controller = new TelegramController(mockService.Object, null);

            //Act
            var result = await controller.AddTransaction(requestId, null);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult);
        }

        [Fact]
        public async Task GetTransactionsByTelegramUserId_ShouldReturnTransactions()
        {
            //Arrange
            var mockService = new Mock<ITelegramUserService>();
            int requestId = 12345;
            var expectedTransactions = new List<CoinTransaction>();

            mockService.Setup(s => s.GetTransactionsByTelegramUserId(requestId)).ReturnsAsync(expectedTransactions);
            var controller = new TelegramController(mockService.Object, null);

            //Act
            var result = await controller.GetTransactionsByTelegramUserId(requestId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var transactions = Assert.IsType<List<CoinTransaction>>(okResult.Value);
            Assert.NotNull(transactions);
        }

        [Fact]
        public async Task GetSummaryOnEveryCoin_ShouldReturnCoinSummaryList()
        {
            //Arrange
            var mockService = new Mock<ITelegramUserService>();
            var requestId = 12345;
            var expectedSummary = new List<CoinSummaryResponse>();

            mockService.Setup(s => s.GetSummaryOnEveryCoin(requestId)).ReturnsAsync(expectedSummary);
            var controller = new TelegramController(mockService.Object, null);

            //Act
            var result = await controller.GetSummaryOnEveryCoin(requestId);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var summary = Assert.IsType<List<CoinSummaryResponse>>(okResult.Value);
            Assert.NotNull(summary);
        }
    }
}
