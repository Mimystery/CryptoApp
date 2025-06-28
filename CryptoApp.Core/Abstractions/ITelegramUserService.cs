using CryptoApp.Core.Models;

namespace CryptoApp.Application.Services
{
    public interface ITelegramUserService
    {
        Task<TelegramUser> GetTelegramUserById(int id);
        Task AddTelegramUser(TelegramUser telegramUser);
        Task<string> LoginTelegramUser(TelegramUser telegramUser);
    }
}