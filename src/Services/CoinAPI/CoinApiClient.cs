using CoinPaprika.Consumer.EndPoints;
using CoinPaprika.Consumer.Interfaces;
using CoinPaprika.Consumer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoinAPI.Consumer
{
    public class CoinApiClient : ICoinApiClient
    {
        private readonly ICoinEndpoint coinEndpoint;

        public CoinApiClient(ICoinEndpoint coinEndpoint)
        {
            this.coinEndpoint = coinEndpoint;
        }

        public async Task<IEnumerable<Coin>> GetCoinsAsync()
        {
            return await coinEndpoint.GetCoins();
        }

        public async Task<Coin> GetCoinAsync()
        {
            return await coinEndpoint.GetCoin();
        }
    }
}
