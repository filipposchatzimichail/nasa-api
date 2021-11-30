using Newtonsoft.Json;
using ApodData = Nasa.Apod.DataAccess.Data.Apod;

namespace Nasa.Apod.DataAccess
{
    public static class Utilities
    {
        public static ApodData GetApodFromJson(string json)
        {
            return JsonConvert.DeserializeObject<ApodData>(json);
        }
    }
}
