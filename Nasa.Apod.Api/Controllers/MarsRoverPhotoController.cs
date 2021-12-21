using Microsoft.AspNetCore.Mvc;
using Nasa.Apod.Business.Interfaces;
using Nasa.Apod.DataAccess.Enums;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Nasa.Apod.Api.Controllers
{
    [ApiController]
    [Route("api/mars-rover-photo")]
    public class MarsRoverPhotoController : ControllerBase
    {
        private readonly IMarsRoverPhotosService _marsRoverPhotosSvc;

        public MarsRoverPhotoController(
            IMarsRoverPhotosService marsRoverPhotosSvc)
        {
            _marsRoverPhotosSvc = marsRoverPhotosSvc;
        }

        [HttpGet]
        public async Task<string> GetMarsRoverPhotosAsync(
            [FromQuery] string rover,
            string date,
            string camera)
        {
            if (!DateTime.TryParse(date, out DateTime parsedDate) ||
                string.IsNullOrEmpty(rover))
            {
                return "Wrong values passed";
            }

            var parsedCamera = string.IsNullOrWhiteSpace(camera) ?
                 (MarsRoverCamera?)null :
                 Enum.Parse<MarsRoverCamera>(camera, true);

            return JsonConvert.SerializeObject(
                await _marsRoverPhotosSvc.GetMarsRoverPhotosAsync(
                    Enum.Parse<MarsRover>(rover, true),
                    parsedDate,
                    parsedCamera));
        }
    }
}
