using Microsoft.AspNetCore.Mvc;
using Nasa.App.DTOs;
using Nasa.Business.Interfaces;
using System.Threading.Tasks;

namespace Nasa.App.Controllers
{
    public class EpicController : Controller
    {
        private readonly IEpicService _epicSvc;

        public EpicController(IEpicService epicSvc)
        {
            _epicSvc = epicSvc;
        }

        [HttpGet]
        public IActionResult EPICImages()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EPICImages(EpicImageDto epicImageDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var epicImages = await _epicSvc
                .GetEpicImagesAsync(
                    epicImageDto.EpicDate?.ToString("yyyy-MM-dd"));

            return View(epicImages);
        }
    }
}
