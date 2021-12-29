using Microsoft.AspNetCore.Mvc;
using Nasa.App.DTOs;
using Nasa.App.Helpers;
using Nasa.Business.Interfaces;
using Nasa.DataAccess.Enums;
using System;
using System.Threading.Tasks;

namespace Nasa.App.Controllers
{
    public class MarsRoverPhotosController : Controller
    {
        private readonly IMarsRoverPhotosService _marsRoverPhotosService;

        public MarsRoverPhotosController(
            IMarsRoverPhotosService marsRoverPhotosService)
        {
            _marsRoverPhotosService = marsRoverPhotosService;
        }

        [HttpGet]
        public IActionResult MarsRoverPhotos()
        {
            PopulateDropdownSelectItems();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MarsRoverPhotos(
            MarsRoverPhotoDto marsRoverPhotoDto)
        {
            PopulateDropdownSelectItems();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _marsRoverPhotosService
                .GetMarsRoverPhotosAsync(
                    (MarsRover)marsRoverPhotoDto.Rover,
                    (DateTime)marsRoverPhotoDto.EarthDate,
                    marsRoverPhotoDto.Camera);

            return View(result);
        }

        private void PopulateDropdownSelectItems()
        {
            ViewBag.Rovers = MarsRover.Spirit.ToSelectList();
            ViewBag.RoverCameras = MarsRoverCamera.MAST.ToSelectList();
        }
    }
}
