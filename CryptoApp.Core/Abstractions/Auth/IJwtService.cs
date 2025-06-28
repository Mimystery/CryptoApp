using CryptoApp.Core.Models;

namespace CryptoApp.Application.Authentication
{
    public interface IJwtService
    {
        string GenerateToken(TelegramUser tgUser);
    }
}