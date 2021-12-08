using Microsoft.AspNetCore.Mvc;
using Nasa.Apod.App.DTOs;
using Nasa.Apod.App.Helpers;
using Nasa.Apod.Business.Interfaces;
using Nasa.Apod.DataAccess.Enums;
using System;
using System.Threading.Tasks;

namespace Nasa.Apod.App.Controllers
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
        public async Task<IActionResult> MarsRoverPhotos(MarsRoverPhotoDto marsRoverPhotoDto)
        {
            PopulateDropdownSelectItems();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _marsRoverPhotosService
                .GetMarsRoverPhotoAsync(MarsRover.Curiosity, DateTime.Parse("2015-06-03"), null);

            return View(result);
        }

        private void PopulateDropdownSelectItems()
        {
            ViewBag.Rovers = MarsRover.Spirit.ToSelectList();
            ViewBag.RoverCameras = MarsRoverCamera.MAST.ToSelectList();
        }
    }
}
