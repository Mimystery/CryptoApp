using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Core.Contracts
{
    public record CoinsListGetResponse(string id, string symbol, string name, string imageUrl)
    {
    }
}
