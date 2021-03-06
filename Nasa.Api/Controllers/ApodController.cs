using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Nasa.Business.Interfaces;
using Newtonsoft.Json;

namespace Nasa.Api.Controllers
{
    [ApiController]
    [Route("api/apod")]
    public class ApodController : ControllerBase
    {
        private readonly IApodService _apodSvc;

        public ApodController(IApodService apodSvc)
        {
            _apodSvc = apodSvc;
        }

        [HttpGet]
        public async Task<string> GetApodAsync()
        {
            return JsonConvert.SerializeObject(
                await _apodSvc.GetApodAsync());
        }

        [HttpGet]
        [Route("date/{date}")]
        public async Task<string> GetApodByDateAsync(string date)
        {
            return JsonConvert.SerializeObject(
                await _apodSvc.GetApodByDateAsync(date));
        }
    }
}
