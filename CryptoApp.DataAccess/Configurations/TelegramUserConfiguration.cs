using CryptoApp.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.DataAccess.Configurations
{
    public class TelegramUserConfiguration : IEntityTypeConfiguration<TelegramUserEntity>
    {
        public void Configure(EntityTypeBuilder<TelegramUserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasMany(u => u.Transactions).WithOne(c => c.User).HasForeignKey(c => c.TelegramUserId).IsRequired();
        }
    }
}
