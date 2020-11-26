using System;
using System.Net.Http;
using ServiceComposer.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationUtils
{
    public static class ViewModelCompositionOptionsExtensions
    {
        public static void RegisterHttpClient<T>(this ViewModelCompositionOptions options, string baseAddress) where T: class
        {
            options.AddServicesConfigurationHandler(typeof(T), (type, services) =>
            {
                services.AddHttpClient<T>(typeof(T).FullName,
                    client => client.BaseAddress = new Uri(baseAddress));
            });
        }

        public static void RegisterHttpClient<T>(this ViewModelCompositionOptions options, Action<IServiceProvider, HttpClient> configureClient) where T: class
        {
            options.AddServicesConfigurationHandler(typeof(T), (type, services) =>
            {
                services.AddHttpClient<T>(typeof(T).FullName, configureClient);
            });
        }
    }
}