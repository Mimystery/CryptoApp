using CryptoApp.Application.Authentication;
using CryptoApp.Core.Models;
using CryptoApp.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Application.Services
{
    public class TelegramUserService : ITelegramUserService
    {
        private readonly ITelegramUserRepository _tgUserRepository;
        private readonly IJwtService _jwtService;

        public TelegramUserService(ITelegramUserRepository tgUserRepository, IJwtService jwtService)
        {
            _tgUserRepository = tgUserRepository;
            _jwtService = jwtService;
        }
        public async Task<TelegramUser> GetTelegramUserById(int id)
        {
            return await _tgUserRepository.GetById(id);
        }

        public async Task<string> LoginTelegramUser(TelegramUser telegramUser)
        {
            var existingTgUser = await _tgUserRepository.GetById(telegramUser.Id);
            if (existingTgUser == null)
            {
                AddTelegramUser(telegramUser);
            }

            return _jwtService.GenerateToken(telegramUser);
        }

        public async Task AddTelegramUser(TelegramUser telegramUser)
        {
            var existingTgUser = await _tgUserRepository.GetById(telegramUser.Id);
            if (existingTgUser != null)
            {
                throw new InvalidOperationException($"Telegram user with ID {telegramUser.Id} already exists.");
            }

            await _tgUserRepository.Add(telegramUser);
        }
    }
}
