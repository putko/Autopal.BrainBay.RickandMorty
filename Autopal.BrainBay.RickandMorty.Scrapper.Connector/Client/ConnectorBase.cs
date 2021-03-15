using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Autopal.BrainBay.RickandMorty.Scrapper.Connector.Client.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Autopal.BrainBay.RickandMorty.Scrapper.Connector.Client
{
    public abstract class ConnectorBase
    {
        protected readonly HttpClient HttpClient;

        protected readonly ILogger<ConnectorBase> Logger;

        protected ConnectorBase(HttpClient httpClient, ILogger<ConnectorBase> logger, IMapper mapper)
        {
            HttpClient = httpClient;
            Logger = logger;
            Mapper = mapper;
        }

        protected IMapper Mapper { get; }

        /// <summary>
        ///     HTTP get async and json deserialization.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        protected async Task<T> Get<T>(string path)
        {
            var response = await HttpClient.GetAsync(path);
            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())
                : default;
        }

        /// <summary>
        ///     Gets all pages objects to single enumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        protected async Task<IEnumerable<T>> GetPages<T>(string url)
        {
            var result = new List<T>();
            var nextPage = -1;

            do
            {
                var dto = await Get<Page<T>>(nextPage == -1
                    ? url
                    : $"{url}{(url.Contains("?") ? "&" : "?")}page={nextPage}");
                result.AddRange(dto.Results);

                nextPage = dto.Info.Next.GetNextPageNumber();
            } while (nextPage != -1);

            return result;
        }

        /// <summary>
        ///     Converts to string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        protected static string ConvertToString(object value, CultureInfo cultureInfo)
        {
            switch (value)
            {
                case null:
                    return string.Empty;
                case Enum:
                {
                    var name = Enum.GetName(value.GetType(), value);
                    if (name == null) return null;
                    var field = value.GetType().GetTypeInfo().GetDeclaredField(name);
                    if (field == null) return name;
                    if (field.GetCustomAttribute(typeof(EnumMemberAttribute)) is
                        EnumMemberAttribute attribute) return attribute.Value ?? name;
                    return name;
                }
                case bool b:
                    return Convert.ToString(b, cultureInfo).ToLowerInvariant();
                case byte[] bytes:
                    return Convert.ToBase64String(bytes);
                default:
                {
                    if (value.GetType().IsArray)
                    {
                        var array = ((Array) value).OfType<object>();
                        return string.Join(",", array.Select(o => ConvertToString(o, cultureInfo)));
                    }

                    break;
                }
            }

            var result = Convert.ToString(value, cultureInfo);
            return result ?? string.Empty;
        }
    }
}