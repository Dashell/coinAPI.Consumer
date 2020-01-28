using CoinAPI.Consumer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinAPI.Consumer.Interfaces
{
    public interface ICoinApiClient
    {
        Task<IEnumerable<Coin>> GetCoinsAsync();
        Task<Coin> GetCoinAsync(string coinId);
    }
}
