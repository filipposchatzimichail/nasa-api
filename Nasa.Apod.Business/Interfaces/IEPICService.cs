using Nasa.Apod.DataAccess.Data.EPIC;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nasa.Apod.Business.Interfaces
{
    public interface IEPICService
    {
        Task<List<EPICImage>> GetEPICImagesAsync();
    }
}
