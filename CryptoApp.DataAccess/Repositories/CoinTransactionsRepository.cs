using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.DataAccess.Repositories
{
    public class CoinTransactionsRepository
    {
        private readonly CoinsDbContext _context;
        private readonly IMapper _mapper;

        public CoinTransactionsRepository(CoinsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //public async Task<>
    }
}
