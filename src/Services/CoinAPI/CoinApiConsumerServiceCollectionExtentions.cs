using CoinAPI.Consumer.Config;
using CoinAPI.Consumer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CoinAPI.Consumer
{
    public static class CoinApiConsumerServiceCollectionExtentions
    {
        public static IServiceCollection AddCoinApiClient(this IServiceCollection services, string xApiKey)
        {
            services.ConfigureEndPoints(xApiKey);
            services.AddTransient<ICoinApiClient, CoinApiClient>();

            return services;
        }
    }
}
