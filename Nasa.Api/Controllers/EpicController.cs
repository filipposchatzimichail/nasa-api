using Microsoft.AspNetCore.Mvc;
using Nasa.Business.Interfaces;
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
        public async Task<string> GetEPICImagesAsync([FromQuery] DateTime? date)
        {
            return await _epicSvc.GetEpicImagesAsJsonStringAsync(date);
        }
    }
}
