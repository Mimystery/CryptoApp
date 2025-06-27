using CryptoApp.Core.Models;

namespace CryptoApp.DataAccess.Repositories;

public interface ICoinsRepository
{
    Task<List<Coin>> GetAllCoinsAsync();
    Task<Coin> GetCoinByIdAsync(string id);
    Task AddCoinAsync(Coin coin);
    Task UpdateCoinAsync(Coin coin);
    Task DeleteCoinAsync(string id);
}