using CoinAPI.Consumer.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CoinAPI.Consumer.EndPoints
{
    public interface ICoinEndpoint
    {
        [Post("/coins")]
        Task<IEnumerable<Coin>> GetCoins();
        [Post("/coin/{coin_id}")]
        Task<Coin> GetCoins(string coin_id);
    }
}
