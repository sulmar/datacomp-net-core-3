using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DataComp.Training.Api.Extensions
{
    // https://www.strathweb.com/2016/09/strongly-typed-configuration-in-asp-net-core-without-ioptionst/
    public static class ServiceCollectionExtensions
    {
        public static TConfig ConfigurePOCO<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var config = new TConfig();
            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }
    }
}
