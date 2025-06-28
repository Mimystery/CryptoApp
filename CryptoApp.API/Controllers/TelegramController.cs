using AutoMapper;
using CryptoApp.Application.Services;
using CryptoApp.Core.Contracts;
using CryptoApp.Core.Models;
using Microsoft.AspNetCore.Mvc;

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
            if (data == null)
            {
                return BadRequest("Invalid authentication data.");
            }

            Console.WriteLine("🔥 Получены данные от Telegram:");
            Console.WriteLine($"ID: {data.Id}");
            Console.WriteLine($"Username: @{data.Username}");
            Console.WriteLine($"Имя: {data.FirstName} {data.LastName}");
            Console.WriteLine($"Фото: {data.PhotoUrl}");
            Console.WriteLine($"Дата авторизации (Unix): {data.AuthDate}");
            Console.WriteLine($"Hash (подпись): {data.Hash}");

            var mappedData = _mapper.Map<TelegramUser>(data);

            var token = await _tgUserService.LoginTelegramUser(mappedData);

            return Ok(token);
        }
    }
}
