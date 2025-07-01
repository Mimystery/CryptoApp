using CryptoApp.Core.Contracts;
using CryptoApp.DataAccess.Configurations;
using CryptoApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.DataAccess
{
    public class CoinsDbContext : DbContext
    {
        public CoinsDbContext(DbContextOptions<CoinsDbContext> options) : base(options)
        {
        }
        public DbSet<CoinEntity> Coins { get; set; }
        public DbSet<TelegramUserEntity> TelegramUsers { get; set; }
        public DbSet<CoinTransactionEntity> CoinTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TelegramUserConfiguration());
            modelBuilder.ApplyConfiguration(new CoinTransactionConfiguration());
        }
    }
}
