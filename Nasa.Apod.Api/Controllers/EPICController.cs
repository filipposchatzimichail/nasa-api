using Microsoft.AspNetCore.Mvc;
using Nasa.Apod.Business.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Nasa.Apod.Api.Controllers
{
    [ApiController]
    [Route("api/epic")]
    public class EPICController : ControllerBase
    {
        private readonly IEPICService _epicSvc;

        public EPICController(IEPICService apodSvc)
        {
            _epicSvc = apodSvc;
        }

        [HttpGet]
        public async Task<string> GetEPICImagesAsync()
        {
            return JsonConvert.SerializeObject(
                await _epicSvc.GetEPICImagesAsync());
        }
    }
}
