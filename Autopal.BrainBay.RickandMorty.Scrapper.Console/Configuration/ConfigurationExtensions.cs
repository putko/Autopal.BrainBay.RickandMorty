using System;
using System.IO;
using Autopal.BrainBay.RickandMorty.Domain;
using Autopal.BrainBay.RickandMorty.Scrapper.Business;
using Autopal.BrainBay.RickandMorty.Scrapper.Connector;
using Autopal.BrainBay.RickandMorty.Scrapper.Console.ServiceProxy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Autopal.BrainBay.RickandMorty.Scrapper.Console.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection SetupServices(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)?.FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            var logLevelFromConfiugration = configuration["Logging:LogLevel:Default"];
            if (!Enum.TryParse(logLevelFromConfiugration, out LogLevel logLevel))
                logLevel = LogLevel.Warning;
            services.AddLogging(opt =>
                {
                    opt.AddFilter((_, level) => level >= logLevel);
                    opt.AddConsole();
                })
                .AddTransient<IScrapperService, ScrapperService>()
                .AddSingleton<IRickandMortyConnector, RickandMortyConnector>()
                .AddTransient<IScrapperServiceProxy, ScrapperServiceProxy>();
            services.AddDbContext<RickandMortyContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("SqlConnection")))
                .AddUnitOfWork<RickandMortyContext>();

            var apiConfiguration = configuration.GetSection(RickandMortyApiSettings.RickandMortyApiSectionName)
                .Get<RickandMortyApiSettings>();
            services.AddHttpClient<IRickandMortyConnector, RickandMortyConnector>(client =>
            {
                client.BaseAddress = new Uri(apiConfiguration.BaseURL);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            if (!Uri.IsWellFormedUriString(apiConfiguration.BaseURL, UriKind.RelativeOrAbsolute))
                throw new Exception($"Api Service url is not correct: {apiConfiguration.BaseURL}");
            services.AddSingleton(apiConfiguration);


            services.AddAutoMapper(typeof(IRickandMortyConnector), typeof(IScrapperService));
            return services;
        }
    }
}