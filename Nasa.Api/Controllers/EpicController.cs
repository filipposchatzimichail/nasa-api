using Microsoft.AspNetCore.Mvc;
using Nasa.Business.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Nasa.Api.Controllers
{
    [ApiController]
    [Route("api/epic")]
    public class EPICController : ControllerBase
    {
        private readonly IEpicService _epicSvc;

        public EPICController(IEpicService apodSvc)
        {
            _epicSvc = apodSvc;
        }

        [HttpGet]
        public async Task<string> GetEPICImagesAsync([FromQuery] string date)
        {
            if (!DateTime.TryParse(date, out DateTime parsedDate))
            {
                return "Wrong value passed";
            }

            return JsonConvert.SerializeObject(
                await _epicSvc.GetEpicImagesAsync(
                    parsedDate.ToString("yyyy-MM-dd")));
        }
    }
}
