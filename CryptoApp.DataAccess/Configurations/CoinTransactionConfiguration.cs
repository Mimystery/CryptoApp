using CryptoApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.DataAccess.Configurations
{
    public class CoinTransactionConfiguration : IEntityTypeConfiguration<CoinTransactionEntity>
    {
        public void Configure(EntityTypeBuilder<CoinTransactionEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.User).WithMany(u => u.Transactions).HasForeignKey(c => c.TelegramUserId).IsRequired();
        }
    }
}
