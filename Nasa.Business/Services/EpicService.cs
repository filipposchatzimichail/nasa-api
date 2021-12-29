using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Nasa.Business.Interfaces;
using Nasa.DataAccess;
using Nasa.DataAccess.Data.Epic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nasa.Business.Services
{
    public class EpicService : IEpicService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        public EpicService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        public async Task<List<EpicImage>> GetEpicImagesAsync(DateTime? date)
        {
            var cacheKey = $"epic-{date:yyyy-MM-dd}";

            var result = _memoryCache.Get<List<EpicImage>>(cacheKey);

            if (result is null)
            {
                result = await GetImagesFromNasa(date);

                _memoryCache.Set(cacheKey, result, TimeSpan.FromHours(12));
            }

            return result;
        }

        public async Task<string> GetEpicImagesAsJsonStringAsync(
            DateTime? date)
        {
            var cacheKey = $"epic-string-null-date";

            if (date is not null)
            {
                cacheKey = $"epic-string-{date:yyyy-MM-dd}";
            }

            var result = _memoryCache.Get<string>(cacheKey);

            if (result is null)
            {
                var epicPhotos = await GetImagesFromNasa(date);

                result = JsonConvert.SerializeObject(epicPhotos);

                _memoryCache.Set(cacheKey, result, TimeSpan.FromHours(12));
            }

            return result;
        }

        private async Task<List<EpicImage>> GetImagesFromNasa(DateTime? date)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress =
                new Uri(_configuration.GetSection("EPIC:BaseUrl").Value);

            var url = $"natural?" +
                $"api_key={_configuration.GetSection("EPIC:ApiKey").Value}";

            if (date is not null)
            {
                url = $"natural/date/{date:yyyy-MM-dd}?" +
                $"api_key={_configuration.GetSection("EPIC:ApiKey").Value}";
            }

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync();

            var epicPhotos = Utilities.GetEpicImagesFromJson(apiResult);

            foreach (var photo in epicPhotos)
            {
                photo.ImageData = await GetEPICImageData(photo);
            }

            return epicPhotos;
        }

        private async Task<string> GetEPICImageData(EpicImage epicImage)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress =
                new Uri(_configuration.GetSection("EPIC:ImageDataUrl").Value);

            var url = $"natural/{epicImage.Date.Year}/{epicImage.Date:MM}/" +
                $"{epicImage.Date.Day}/jpg/{epicImage.Name}.jpg?" +
                $"api_key={_configuration.GetSection("EPIC:ApiKey").Value}";

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var photoByteArray = await response.Content.ReadAsByteArrayAsync();

            var photo = Convert.ToBase64String(photoByteArray);

            return photo;
        }
    }
}
