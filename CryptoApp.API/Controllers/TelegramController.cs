using AutoMapper;
using CryptoApp.Application.Services;
using CryptoApp.Core.Contracts;
using CryptoApp.Core.Models;
using CryptoApp.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramController : ControllerBase
    {
        private readonly ITelegramUserService _tgUserService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public TelegramController(ITelegramUserService tgUserService, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _tgUserService = tgUserService;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        [HttpPost("auth")]
        public async Task<ActionResult<TelegramAuthResponse>> Authenticate([FromBody] TelegramAuthResponse data)
        {
            if (!_tgUserService.VerifyTelegramAuth(data))
            {
                return Unauthorized("Invalid data");
            }

            var mappedData = _mapper.Map<TelegramUser>(data);

            var token = await _tgUserService.LoginTelegramUser(mappedData);

            //_httpContext.HttpContext.Response.Cookies.Append("first-cookies", token);

            return Ok(new { token });
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<TelegramUser>> GetTelegramUser(int id)
        {
            var user = await _tgUserService.GetTelegramUserById(id);
            if (user == null)
            {
                return NotFound("Telegram user not found");
            }

            return Ok(user);
        }

        [HttpPost("user/{telegramUserId}/transaction")]
        public async Task<ActionResult> AddTransaction(int telegramUserId, [FromBody] CoinTransactionRequest coinTransaction)
        {
            await _tgUserService.AddTransaction(telegramUserId, coinTransaction);

            return Ok("Transaction added successfully");
        }

        [HttpGet("user/{telegramUserId}/transaction")]
        public async Task<ActionResult<List<CoinTransaction>>> GetTransactionsByTelegramUserId(int telegramUserId)
        {
            var transactions = await _tgUserService.GetTransactionsByTelegramUserId(telegramUserId);
            if (transactions == null || !transactions.Any())
            {
                return NotFound("No transactions found for this user");
            }

            return Ok(transactions);
        }

        [HttpGet("user/{telegramUserId}/summary")]
        public async Task<ActionResult<List<CoinSummaryResponse>>> GetSummaryOnEveryCoin(int telegramUserId)
        {
            var summary = await _tgUserService.GetSummaryOnEveryCoin(telegramUserId);
            if (summary == null || !summary.Any())
            {
                return NotFound("No coin summary found for this user");
            }

            return Ok(summary);
        }
    }
}
