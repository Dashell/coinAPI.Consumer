using CoinPaprika.Consumer.EndPoints;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace CoinPaprika.Consumer.Config
{
    public static class EndpointsConfig
    {
        public static IServiceCollection ConfigureEndPoints(this IServiceCollection services, string xApiKey)
        {

            const string BASE_URL = "https://rest.coinapi.io/";

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandlers", false);
            }
            HttpMessageHandler coinApiAuthenticationFactory() => new AuthenticatedHttpClientHandler
            {
                XApiKey = xApiKey
            };

            services.AddRefitClient<ICoinEndpoint>().ConfigureHttpClient(c => c.BaseAddress = new Uri(BASE_URL)).ConfigurePrimaryHttpMessageHandler(coinApiAuthenticationFactory);
            return services;
        }

        public class AuthenticatedHttpClientHandler : HttpClientHandler
        {
            public string? XApiKey { get; set; }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("X-CoinAPI-Key", XApiKey);
                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}
