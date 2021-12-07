using Microsoft.AspNetCore.Http;
using Nasa.Apod.Business.Interfaces;
using Nasa.Apod.DataAccess;
using Nasa.Apod.DataAccess.Data;
using Nasa.Apod.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nasa.Apod.Business.Services
{
    public class MarsRoverPhotosService : IMarsRoverPhotosService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MarsRoverPhotosService(
            IHttpClientFactory httpClientFactory, 
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<MarsRoverPhoto>> GetMarsRoverPhotoAsync(
            MarsRover rover, 
            DateTime earthTime,
            MarsRoverCamera? camera = null)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var request = _httpContextAccessor.HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/api/" +
                $"mars-rover-photo?rover={rover}&date={earthTime:yyyy-MM-dd}";

            if (camera != null)
            {
                url += $"camera={camera}";
            }

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = Utilities.GetMarsRoverPhotoFromJson(json);

            return result;
        }
    }
}
