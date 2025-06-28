using AutoMapper;
using CryptoApp.Core.Contracts;
using CryptoApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Application.Mappings
{
    public class TelegramUserMappingProfile : Profile
    {
        public TelegramUserMappingProfile()
        {
            CreateMap<TelegramUserEntity, TelegramUser>();
            CreateMap<TelegramAuthResponse, TelegramUser>();
        }
    }
}
