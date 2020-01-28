using CoinAPI.Consumer.Exceptions;
using CoinAPI.Consumer.EndPoints;
using CoinAPI.Consumer.Interfaces;
using CoinAPI.Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            try
            {
                return await coinEndpoint.GetCoins();
            }
            catch (Refit.ApiException apiException)
            {
                if (apiException.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new AuthenticationFailedException();
                }
                throw new Exception("unhandled server exception");
            }
        }

        public async Task<Coin> GetCoinAsync(string coinId)
        {
            try
            {
                IEnumerable<Coin> coins = await coinEndpoint.GetCoins();

                Coin coin = coins.FirstOrDefault(c => c.AssetId.Equals(coinId, StringComparison.OrdinalIgnoreCase));

                if (coin == null)
                {
                    throw new CoinNotFoundException(coinId);
                }

                return coin;
            }
            catch(Refit.ApiException apiException)
            {
                if(apiException.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new AuthenticationFailedException();
                }
                throw new Exception("unhandled server exception");
            }
        }
    }
}
