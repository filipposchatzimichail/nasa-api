using System.Threading.Tasks;
using Nasa.DataAccess.Data.Apod;

namespace Nasa.Business.Interfaces
{
    public interface IApodService
    {
        Task<ApodImage> GetApodAsync();
        Task<ApodImage> GetApodByDateAsync(string date);
    }
}
