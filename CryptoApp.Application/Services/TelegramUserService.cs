using CryptoApp.Application.Authentication;
using CryptoApp.Core.Contracts;
using CryptoApp.Core.Models;
using CryptoApp.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
                await AddTelegramUser(telegramUser);
            }

            return _jwtService.GenerateToken(telegramUser);
        }

        public bool VerifyTelegramAuth(TelegramAuthResponse telegramUser)
        {
            var botToken = "7600592513:AAGDpmp_GIzFd6VmD61Ha0S-kCyUnX4xCJ8";

            var dataCheckString = $"auth_date={telegramUser.AuthDate}\n" +
                                  $"first_name={telegramUser.FirstName}\n" +
                                  (telegramUser.LastName != null ? $"last_name={telegramUser.LastName}\n" : "") +
                                  $"id={telegramUser.Id}\n" +
                                  (telegramUser.PhotoUrl != null ? $"photo_url={telegramUser.PhotoUrl}\n" : "") +
                                  (telegramUser.Username != null ? $"username={telegramUser.Username}\n" : "");

            dataCheckString = dataCheckString.TrimEnd('\n');

            var secretKey = Encoding.UTF8.GetBytes(
                Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(botToken)))
            );

            using var hmac = new HMACSHA256(secretKey);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataCheckString));
            var hexHash = BitConverter.ToString(hash).Replace("-", "").ToLower();

            return hexHash == telegramUser.Hash.ToLower();
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
