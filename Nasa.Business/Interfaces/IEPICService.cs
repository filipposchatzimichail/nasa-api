using Nasa.DataAccess.Data.Epic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nasa.Business.Interfaces
{
    public interface IEpicService
    {
        Task<List<EpicImage>> GetEpicImagesAsync();
    }
}
