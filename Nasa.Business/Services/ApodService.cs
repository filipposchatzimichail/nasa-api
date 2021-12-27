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

        public ApodService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<ApodImage> GetApodAsync()
        {
            var result = await Caching.GetObjectFromCache<Task<ApodImage>>("apod", 100, GetApodFromNasaAsync);

            return result;
        }

        public async Task<ApodImage> GetApodByDateAsync(string date)
        {
            var result = await Caching.GetObjectFromCache<Task<ApodImage>>($"apod-date-{date}", 100, date, GetApodFromNasaByDateAsync);

            return result;
        }


        private async Task<ApodImage> GetApodFromNasaAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration.GetSection("Apod:BaseUrl").Value);

            var response = await httpClient.GetAsync($"?api_key={_configuration.GetSection("Apod:ApiKey").Value}");
            response.EnsureSuccessStatusCode();

            var jsonData = await response.Content.ReadAsStringAsync();

            var result = Utilities.GetApodFromJson(jsonData);

            return result;
        }

        private async Task<ApodImage> GetApodFromNasaByDateAsync(string date)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration.GetSection("Apod:BaseUrl").Value);

            var response = await httpClient.GetAsync($"?api_key={_configuration.GetSection("Apod:ApiKey").Value}&date={date}");
            response.EnsureSuccessStatusCode();

            var jsonData = await response.Content.ReadAsStringAsync();

            var result = Utilities.GetApodFromJson(jsonData);

            return result;
        }

    }
}