using CoinPaprika.Consumer.Config;
using CoinPaprika.Consumer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CoinPaprika.Consumer
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
