using Microsoft.Extensions.Configuration;
using Nasa.Business.Interfaces;
using Nasa.DataAccess;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nasa.DataAccess.Data.Apod;

namespace Nasa.Business.Services
{
    public class ApodService : IApodService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ApodService(
            IHttpClientFactory httpClientFactory, 
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<ApodImage> GetApodAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration.GetSection("Apod:BaseUrl").Value);

            var response = await httpClient
                .GetAsync($"?api_key={_configuration.GetSection("Apod:ApiKey").Value}");
            response.EnsureSuccessStatusCode();

            var photo = await response.Content.ReadAsStringAsync();

            var result = Utilities.GetApodFromJson(photo);

            return result;
        }

        public async Task<ApodImage> GetApodByDateAsync(string date)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration.GetSection("Apod:BaseUrl").Value);

            var response = await httpClient
                .GetAsync($"?api_key={_configuration.GetSection("Apod:ApiKey").Value}&date={date}");
            response.EnsureSuccessStatusCode();

            var photo = await response.Content.ReadAsStringAsync();

            var result = Utilities.GetApodFromJson(photo);

            return result;
        }
    }
}