using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoApp.Core.Models;
using CryptoApp.DataAccess.Repositories;

namespace CryptoApp.Application.Services
{
    public class CoinsService : ICoinsService
    {
        private readonly ICoinsRepository _coinsRepository;

        public CoinsService(ICoinsRepository coinsRepository)
        {
            _coinsRepository = coinsRepository;
        }

        public async Task<List<Coin>> GetAllCoinsAsync()
        {
            return await _coinsRepository.GetAllCoinsAsync();
        }
        public async Task<Coin> GetCoinByIdAsync(string id)
        {
            return await _coinsRepository.GetCoinByIdAsync(id);
        }
        public async Task AddCoinAsync(Coin coin)
        {
            var existingCoin = await _coinsRepository.GetCoinByIdAsync(coin.Id);
            if (existingCoin != null)
            {
                throw new InvalidOperationException($"Coin with ID {coin.Id} already exists.");
            }

            await _coinsRepository.AddCoinAsync(coin);
        }
        public async Task UpdateCoinAsync(Coin coin)
        {
            await _coinsRepository.UpdateCoinAsync(coin);
        }
        public async Task DeleteCoinAsync(string id)
        {
            await _coinsRepository.DeleteCoinAsync(id);
        }
    }
}
