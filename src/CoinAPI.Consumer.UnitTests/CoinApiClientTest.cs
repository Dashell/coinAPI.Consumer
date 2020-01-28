using CoinAPI.Consumer.EndPoints;
using CoinAPI.Consumer.Exceptions;
using CoinAPI.Consumer.Interfaces;
using CoinAPI.Consumer.Models;
using CoinAPI.Consumer.UnitTests.Fixtures;
using FluentAssertions;
using Moq;
using Refit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CoinAPI.Consumer.UnitTests
{
    public class CoinApiClientTest
    {
        private readonly ICoinApiClient client;
        private readonly Mock<ICoinEndpoint> coinEndpoint;


        public CoinApiClientTest()
        {

            coinEndpoint = new Mock<ICoinEndpoint>();
            client = new CoinApiClient(coinEndpoint.Object);
        }


        [Fact]
        public void GetCoinsAsync_should_throw_AuthenticationFailedException_when_coinEndpoint_throw_refitException_with_401_statuscode()
        {
            // arrange
            ApiException refitException = GlobalFixture.CreateRefitApiException(HttpStatusCode.Unauthorized, HttpMethod.Get);
            // mock
            coinEndpoint.Setup(mock => mock.GetCoins()).Throws(refitException);
            // act
            Func<Task> result = async () => { await client.GetCoinsAsync(); };

            // assert
            result.Should().Throw<AuthenticationFailedException>();
        }
        
        [Fact]
        public void GetCoinsAsync_should_throw_Exception_with_common_mesage_when_coinEndpoint_throw_refitException_with_non_401_statuscode()
        {
            // arrange
            ApiException refitException = GlobalFixture.CreateRefitApiException(HttpStatusCode.Unauthorized, HttpMethod.Get);
            // mock
            coinEndpoint.Setup(mock => mock.GetCoins()).Throws(refitException);
            // act
            Func<Task> result = async () => { await client.GetCoinsAsync(); };

            // assert
            result.Should().Throw<Exception>().And.Message.Contains("unhandled server exception");
        }

        [Fact]
        public async Task GetCoinsAsync_should_return_coins_list()
        {
            // arrange
            IEnumerable<Coin> coins = GlobalFixture.ValidCoinList();
            // mock
            coinEndpoint.Setup(mock => mock.GetCoins()).Returns(Task.FromResult(coins));
            // act
            IEnumerable<Coin> result = await client.GetCoinsAsync();

            // assert
            result.Should().BeEquivalentTo(coins);
        }

        [Fact]
        public void GetCoinAsync_should_throw_AuthenticationFailedException_when_coinEndpoint_throw_refitException_with_401_statuscode()
        {
            // arrange
            string coindId = "BTC";
            ApiException refitException = GlobalFixture.CreateRefitApiException(HttpStatusCode.Unauthorized, HttpMethod.Get);
            // mock
            coinEndpoint.Setup(mock => mock.GetCoins()).Throws(refitException);
            // act
            Func<Task> result = async () => { await client.GetCoinAsync(coindId); };

            // assert
            result.Should().Throw<AuthenticationFailedException>();
        }

        [Fact]
        public void GetCoinAsync_should_throw_CoinNotFoundException_when_coin_not_found_by_id()
        {
            // arrange
            string coindId = "UNK";
            IEnumerable<Coin> coins = GlobalFixture.ValidCoinList();
            // mock
            coinEndpoint.Setup(mock => mock.GetCoins()).Returns(Task.FromResult(coins));
            // act
            Func<Task> result = async () => { await client.GetCoinAsync(coindId); };

            // assert
            result.Should().Throw<CoinNotFoundException>();
        }

        [Fact]
        public async Task GetCoinAsync_should_return_coin_for_specific_id()
        {
            // arrange
            string coindId = "BTC";
            IEnumerable<Coin> coins = GlobalFixture.ValidCoinList();
            // mock
            coinEndpoint.Setup(mock => mock.GetCoins()).Returns(Task.FromResult(coins));
            // act
            Coin result = await client.GetCoinAsync(coindId);

            // assert
            result.AssetId.Should().Be(coindId);
            result.Name.Should().Be("Bitcoin");
        }
    }
}
