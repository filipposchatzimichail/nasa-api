using System.Threading.Tasks;
using ApodData = Nasa.Apod.DataAccess.Data.Apod;

namespace Nasa.Apod.Business.Interfaces
{
    public interface IApodService
    {
        Task<ApodData> GetApodAsync();
        Task<ApodData> GetApodByDateAsync(string date);
    }
}
