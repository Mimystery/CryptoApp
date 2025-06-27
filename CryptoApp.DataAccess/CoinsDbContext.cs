using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoApp.DataAccess
{
    public class CoinsDbContext : DbContext
    {
        public CoinsDbContext(DbContextOptions<CoinsDbContext> options) : base(options)
        {
        }
        public DbSet<CoinEntity> Coins { get; set; }
    }
}
