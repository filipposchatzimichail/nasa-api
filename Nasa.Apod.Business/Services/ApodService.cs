using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApodService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApodData> GetApodAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();
            
            var request = _httpContextAccessor.HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/apod";

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = Utilities.GetApodFromJson(json);

            return result;
        }

        public async Task<ApodData> GetApodByDateAsync(string date)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var request = _httpContextAccessor.HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/apod/date/{date}";

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = Utilities.GetApodFromJson(json);

            return result;
        }
    }
}




