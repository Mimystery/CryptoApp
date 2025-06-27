using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CryptoApp.Core.Models;
using CryptoApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoApp.DataAccess.Repositories
{
    public class CoinsRepository : ICoinsRepository
    {
        private readonly CoinsDbContext _context;
        private readonly IMapper _mapper;

        public CoinsRepository(CoinsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Coin>> GetAllCoinsAsync()
        {
            var coinEntities = await _context.Coins.ToListAsync();
            return _mapper.Map<List<Coin>>(coinEntities);
        }

        public async Task<Coin> GetCoinByIdAsync(string id)
        {
            var coinEntity = await _context.Coins.FindAsync(id);
            return _mapper.Map<Coin>(coinEntity);
        }

        public async Task AddCoinAsync(Coin coin)
        {
            var coinEntity = new CoinEntity
            {
                Id = coin.Id,
                Symbol = coin.Symbol,
                Name = coin.Name,
                ImageUrl = coin.ImageUrl,
            };

            await _context.Coins.AddAsync(coinEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCoinAsync(Coin coin)
        {
            var coinEntity = await _context.Coins.FindAsync(coin.Id);
            if (coinEntity == null) return;
            coinEntity.Symbol = coin.Symbol;
            coinEntity.Name = coin.Name;
            coinEntity.ImageUrl = coin.ImageUrl;
            _context.Coins.Update(coinEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCoinAsync(string id)
        {
            await _context.Coins.Where(c => c.Id == id).ExecuteDeleteAsync();
        }
    }
}
