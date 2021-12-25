using Microsoft.Extensions.Configuration;
using Nasa.Business.Interfaces;
using Nasa.DataAccess;
using Nasa.DataAccess.Data.MarsRover;
using Nasa.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nasa.Business.Services
{
    public class MarsRoverPhotosService : IMarsRoverPhotosService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public MarsRoverPhotosService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<List<MarsRoverPhoto>> GetMarsRoverPhotosAsync(
            MarsRover rover,
            DateTime earthTime,
            MarsRoverCamera? camera = null)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress =
                new Uri(_configuration.GetSection("MarsRoverPhotos:BaseUrl").Value);

            var url = $"{rover}/photos?earth_date={earthTime:yyyy-MM-dd}&" +
                $"api_key={_configuration.GetSection("MarsRoverPhotos:ApiKey").Value}";

            if (camera != null)
            {
                url += $"&camera={camera}";
            }

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var apiResult = await response.Content.ReadAsStringAsync();

            var result = Utilities.GetMarsRoverPhotoFromJson(apiResult);

            return result;
        }
    }
}
