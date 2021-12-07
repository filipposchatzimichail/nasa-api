using ApodData = Nasa.Apod.DataAccess.Data.Apod;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Nasa.Apod.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApodController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ApodController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<string> GetApodAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration.GetSection("Apod:BaseUrl").Value);

            var response = await httpClient
                .GetAsync($"?api_key={_configuration.GetSection("Apod:ApiKey").Value}");
            response.EnsureSuccessStatusCode();
                        
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        [HttpGet]
        [Route("date/{date}")]
        public async Task<string> GetApodByDateAsync(string date)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration.GetSection("Apod:BaseUrl").Value);

            var response = await httpClient
                .GetAsync($"?api_key={_configuration.GetSection("Apod:ApiKey").Value}&date={date}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
