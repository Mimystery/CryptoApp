using AutoMapper;
using CryptoApp.Core.Contracts;
using CryptoApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.DataAccess.Repositories
{
    public class TelegramUserRepository : ITelegramUserRepository
    {
        private readonly CoinsDbContext _context;
        private readonly IMapper _mapper;

        public TelegramUserRepository(CoinsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TelegramUser> GetById(int id)
        {
            var telegramUser = await _context.TelegramUsers.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            return _mapper.Map<TelegramUser>(telegramUser);
        }

        public async Task Add(TelegramUser telegramUser)
        {
            var telegramUserEntity = new TelegramUserEntity
            {
                Id = telegramUser.Id,
                Username = telegramUser.Username,
                FirstName = telegramUser.FirstName,
                LastName = telegramUser.LastName,
                AuthDate = telegramUser.AuthDate,
                Hash = telegramUser.Hash,
                PhotoUrl = telegramUser.PhotoUrl
            };

            await _context.TelegramUsers.AddAsync(telegramUserEntity);
            await _context.SaveChangesAsync();
        }
    }
}
