using Microsoft.Extensions.Configuration;
using Nasa.Apod.Business.Interfaces;
using Nasa.Apod.DataAccess;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApodData = Nasa.Apod.DataAccess.Data.Apod;

namespace Nasa.Apod.Business.Services
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

        public async Task<ApodData> GetApodAsync()
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

        public async Task<ApodData> GetApodByDateAsync(string date)
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