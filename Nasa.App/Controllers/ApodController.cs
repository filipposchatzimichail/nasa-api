using Microsoft.AspNetCore.Mvc;
using Nasa.App.Models;
using Nasa.Business.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Nasa.App.Controllers
{
    [Route("")]
    [Route("apod")]
    public class ApodController : Controller
    {
        private readonly IApodService _apodService;
        public ApodController(IApodService apodService)
        {
            _apodService = apodService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var result = await _apodService.GetApodAsync();

            return View(result);
        }

        [HttpGet]
        [Route("date/{date}")]
        public async Task<IActionResult> GetApodByDate(string date)
        {
            var result = await _apodService.GetApodByDateAsync(date);

            return View("Index", result);
        }
         
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
