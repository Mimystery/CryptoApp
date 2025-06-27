using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoApp.DataAccess.Configurations
{
    public class CoinConfiguration : IEntityTypeConfiguration<CoinEntity>
    {
        public void Configure(EntityTypeBuilder<CoinEntity> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
