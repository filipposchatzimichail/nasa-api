using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> EPICImages()
        {
            var epicImages = await _epicSvc.GetEpicImagesAsync();

            return View(epicImages);
        }
    }
}
