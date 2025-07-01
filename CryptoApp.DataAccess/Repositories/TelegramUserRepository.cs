using AutoMapper;
using CryptoApp.Core.Contracts;
using CryptoApp.Core.Models;
using CryptoApp.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
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
                PhotoUrl = telegramUser.PhotoUrl,
            };

            await _context.TelegramUsers.AddAsync(telegramUserEntity);
            await _context.SaveChangesAsync();
        }

        public async Task AddTransaction(int telegramUserId, CoinTransactionRequest coinTransaction)
        {
            var user = await _context.TelegramUsers.Include(u => u.Transactions).FirstOrDefaultAsync(u => u.Id == telegramUserId);

            var transaction = new CoinTransactionEntity
            {
                Id = Guid.NewGuid().ToString(),
                TelegramUserId = telegramUserId,
                CoinId = coinTransaction.CoinId,
                Symbol = coinTransaction.Symbol,
                Name = coinTransaction.Name,
                ImageUrl = coinTransaction.ImageUrl,
                Amount = coinTransaction.Amount,
                Price = coinTransaction.Price,
                TransactionDate = coinTransaction.TransactionDate
            };

            if (user == null)
            {
                throw new InvalidOperationException($"Telegram user with ID {telegramUserId} does not exist.");
            }

            user.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CoinTransaction>> GetTransactionsByUserId(int telegramUserId)
        {
            var transactions = await _context.CoinTransactions
                .Where(t => t.TelegramUserId == telegramUserId).ToListAsync();
            
            return _mapper.Map<List<CoinTransaction>>(transactions);
        }
        public async Task DeleteTransaction(string transactionId)
        {
            var transaction = await _context.CoinTransactions
                .FirstOrDefaultAsync(t => t.Id == transactionId);
            if (transaction == null)
            {
                throw new InvalidOperationException($"Transaction with ID {transactionId} for user does not exist.");
            }
            _context.CoinTransactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteNullIdTransactions(int userId)
        {
            var transactionsToDelete = await _context.CoinTransactions
                .Where(t => t.Id == null && t.TelegramUserId == userId)
                .ToListAsync();

            if (transactionsToDelete.Any())
            {
                _context.CoinTransactions.RemoveRange(transactionsToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
