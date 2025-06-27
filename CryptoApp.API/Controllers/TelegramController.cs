using CryptoApp.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CryptoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramController : ControllerBase
    {
        [HttpPost("auth")]
        public ActionResult<TelegramAuthResponse> Authenticate([FromBody] TelegramAuthResponse data)
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

            return Ok(data);
        }
    }
}
