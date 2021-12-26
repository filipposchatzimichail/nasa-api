using Microsoft.Extensions.Configuration;
using Nasa.Business.Interfaces;
using Nasa.DataAccess;
using Nasa.DataAccess.Data.Epic;
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

        public EpicService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<List<EpicImage>> GetEpicImagesAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress =
                new Uri(_configuration.GetSection("EPIC:BaseUrl").Value);

            var url = $"natural/date/2019-05-30?" +
                $"api_key={_configuration.GetSection("EPIC:ApiKey").Value}";

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
