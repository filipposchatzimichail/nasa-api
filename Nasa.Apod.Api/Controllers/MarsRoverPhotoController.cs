using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nasa.Apod.Api.Controllers
{
    [ApiController]
    [Route("api/mars-rover-photo")]
    public class MarsRoverPhotoController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public MarsRoverPhotoController(
            IHttpClientFactory httpClientFactory, 
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<string> GetMarsRoverPhotoAsync(
            [FromQuery] string rover, 
            string date, 
            string camera)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = 
                new Uri(_configuration.GetSection("MarsRoverPhotos:BaseUrl").Value);

            if (!DateTime.TryParse(date, out DateTime parsedDate) ||
                string.IsNullOrEmpty(rover))
            {
                return "Wrong values passed";
            }

            var url = $"{rover}/photos?earth_date={date}&" +
                $"api_key={_configuration.GetSection("MarsRoverPhotos:ApiKey").Value}";

            if (!string.IsNullOrEmpty(camera))
            {
                url += $"&camera={camera}";
            }

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
