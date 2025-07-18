﻿using AutoMapper;
using CryptoApp.Application.Services;
using CryptoApp.Core.Contracts;
using CryptoApp.Core.Models;
using CryptoApp.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramController : ControllerBase
    {
        private readonly ITelegramUserService _tgUserService;
        private readonly IMapper _mapper;

        public TelegramController(ITelegramUserService tgUserService, IMapper mapper)
        {
            _tgUserService = tgUserService;
            _mapper = mapper;
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
            if (coinTransaction == null)
            {
                return BadRequest("coinTransaction is null");
            }

            try
            {
                Console.WriteLine($"Received transaction: CoinId={coinTransaction.CoinId}, Symbol={coinTransaction.Symbol}, Amount={coinTransaction.Amount}, Date={coinTransaction.TransactionDate}, TgUserId={telegramUserId}");
                await _tgUserService.AddTransaction(telegramUserId, coinTransaction);
                return Ok("Transaction added successfully");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error in AddTransaction: {ex}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("user/{telegramUserId}/transaction")]
        public async Task<ActionResult<List<CoinTransaction>>> GetTransactionsByTelegramUserId(int telegramUserId)
        {
            var transactions = await _tgUserService.GetTransactionsByTelegramUserId(telegramUserId);

            return Ok(transactions);
        }

        [HttpGet("user/{telegramUserId}/summary")]
        public async Task<ActionResult<List<CoinSummaryResponse>>> GetSummaryOnEveryCoin(int telegramUserId)
        {
            var summary = await _tgUserService.GetSummaryOnEveryCoin(telegramUserId);

            return Ok(summary);
        }

        [HttpDelete("user/{transactionId}/transaction")]
        public async Task<ActionResult> DeleteTransactionById(Guid transactionId)
        { 
            await _tgUserService.DeleteTransactionById(transactionId);
            return Ok("Transaction deleted successfully");
        }

        [HttpDelete("user/{userId}/transaction/nulls")]
        public async Task<IActionResult> DeleteNullIdTransactions(int userId)
        {
            await _tgUserService.DeleteNullIdTransactions(userId);
            return Ok("Null ID transactions deleted successfully");
        }
    }
}
