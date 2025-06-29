using AutoMapper;
using CryptoApp.Application.Services;
using CryptoApp.Core.Contracts;
using CryptoApp.Core.Models;
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

            return Ok(new {token});
        }
    }
}
