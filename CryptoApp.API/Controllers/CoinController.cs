using System.Text.Json;
using CryptoApp.Application.Services;
using CryptoApp.Core.Contracts;
using CryptoApp.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CoinController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ICoinsService _coinsService;

        public CoinController(IHttpClientFactory httpClientFactory, ICoinsService coinsService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _coinsService = coinsService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCoinGecko(string id)
        {
            var url = $"https://api.coingecko.com/api/v3/coins/{id}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, "Ошибка при запросе к CoinGecko");

                var content = await response.Content.ReadAsStringAsync();

                var coinGeckoCoin = JsonSerializer.Deserialize<CoinGeckoResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (coinGeckoCoin == null)
                    return NotFound("Криптовалюта не найдена");

                var coin = new Coin(coinGeckoCoin.id, coinGeckoCoin.symbol, coinGeckoCoin.name,
                    coinGeckoCoin.image.small);

                await _coinsService.AddCoinAsync(coin);

                return Ok(coin);

            }
            catch
            {
                throw new InvalidOperationException($"Coin with ID {id} already exists.");
            }
        }

        [Authorize]
        [HttpGet("list")]
        public async Task<ActionResult<List<Coin>>> GetCoinList()
        {
            var coins = await _coinsService.GetAllCoinsAsync();

            return Ok(coins);
        }
    }
}
