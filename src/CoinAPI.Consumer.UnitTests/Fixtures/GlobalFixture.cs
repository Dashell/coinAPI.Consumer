using CoinAPI.Consumer.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CoinAPI.Consumer.UnitTests.Fixtures
{
    public static class GlobalFixture
    {
        internal static ApiException CreateRefitApiException(HttpStatusCode statusCode, HttpMethod method, string reasonPhrase = "Not Specified")
        {
            HttpResponseMessage response = new HttpResponseMessage(statusCode)
            {
                ReasonPhrase = reasonPhrase
            };
            return Refit.ApiException.Create(new HttpRequestMessage(), method, response).Result;
        }

        internal static IEnumerable<Coin> ValidCoinList()
        {
            return new List<Coin> {
                new Coin
                {
                    AssetId = "BTC",
                    Name="Bitcoin"
                },
                new Coin
                {
                    AssetId = "ETH",
                    Name="Ethereum"
                }
            };
        }
    }
}
