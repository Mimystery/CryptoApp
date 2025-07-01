using AutoMapper;
using CryptoApp.Core.Models;
using CryptoApp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoApp.Core.Contracts;

namespace CryptoApp.Application.Mappings
{
    public class CoinMappingProfile : Profile
    {
        public CoinMappingProfile()
        {
            CreateMap<CoinEntity, Coin>();
            CreateMap<Coin, CoinGeckoResponse>();
            CreateMap<CoinTransactionRequest, CoinTransactionEntity>();
            CreateMap<CoinTransactionEntity, CoinTransaction>().ConstructUsing(ent => new CoinTransaction(
                ent.Id, ent.TelegramUserId, ent.CoinId, ent.Symbol, ent.Name, ent.ImageUrl, ent.Amount, ent.Price,
                ent.TransactionDate));
        }
    }
}
