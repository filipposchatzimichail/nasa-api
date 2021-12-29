using Microsoft.Extensions.Configuration;
using Nasa.Business.Interfaces;
using Nasa.DataAccess;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nasa.DataAccess.Data.Apod;
using Microsoft.Extensions.Caching.Memory;

namespace Nasa.Business.Services
{
    public class ApodService : IApodService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        public ApodService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        public async Task<ApodImage> GetApodAsync()
        {
            var cacheKey = "apod";
            var result = _memoryCache.Get<ApodImage>(cacheKey);

            if (result is null)
            {
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.BaseAddress = new Uri(_configuration.GetSection("Apod:BaseUrl").Value);

                var response = await httpClient.GetAsync($"?api_key={_configuration.GetSection("Apod:ApiKey").Value}");
                response.EnsureSuccessStatusCode();

                var jsonData = await response.Content.ReadAsStringAsync();

                result = Utilities.GetApodFromJson(jsonData);

                _memoryCache.Set(cacheKey, result, TimeSpan.FromHours(12));
            }

            return result;
        }

        public async Task<ApodImage> GetApodByDateAsync(string date)
        {
            var cacheKey = $"apod-date-{date}";
            var result = _memoryCache.Get<ApodImage>(cacheKey);

            if (result is null)
            {
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.BaseAddress = new Uri(_configuration.GetSection("Apod:BaseUrl").Value);

                var response = await httpClient.GetAsync($"?api_key={_configuration.GetSection("Apod:ApiKey").Value}&date={date}");
                response.EnsureSuccessStatusCode();

                var jsonData = await response.Content.ReadAsStringAsync();

                result = Utilities.GetApodFromJson(jsonData);

                _memoryCache.Set(cacheKey, result, TimeSpan.FromHours(6));
            }

            return result;
        }
    }
}