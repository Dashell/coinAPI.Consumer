using CoinPaprika.Consumer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinPaprika.Consumer.Interfaces
{
    public interface ICoinApiClient
    {
        Task<IEnumerable<Coin>> GetCoinsAsync();
        Task<Coin> GetCoinAsync(string coinId);
    }
}
