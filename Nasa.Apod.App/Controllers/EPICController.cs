using Microsoft.AspNetCore.Mvc;
using Nasa.Apod.Business.Interfaces;
using System.Threading.Tasks;

namespace Nasa.Apod.App.Controllers
{
    public class EPICController : Controller
    {
        private readonly IEPICService _epicSvc;

        public EPICController(
            IEPICService epicSvc)
        {
            _epicSvc = epicSvc;
        }

        [HttpGet]
        public async Task<IActionResult> EPICImages()
        {
            var epicImages = await _epicSvc.GetEPICImagesAsync();

            return View(epicImages);
        }
    }
}
