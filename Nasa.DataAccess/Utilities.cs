using Nasa.DataAccess.Data.Epic;
using Nasa.DataAccess.Data.MarsRover;
using Nasa.DataAccess.Data.Apod;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Nasa.DataAccess
{
    public static class Utilities
    {
        public static ApodImage GetApodFromJson(string json)
        {
            return JsonConvert.DeserializeObject<ApodImage>(json);
        }

        public static List<MarsRoverPhoto> GetMarsRoverPhotoFromJson(string json)
        {
            var photos = JObject.Parse(json).SelectToken("photos").ToString();

            var marsRoverPhotos = JsonConvert.DeserializeObject<List<MarsRoverPhoto>>(photos);

            return marsRoverPhotos;
        }

        public static List<EPICImage> GetEpicImagesFromJson(string json)
        {
            var epicImages = JsonConvert.DeserializeObject<List<EPICImage>>(json);

            return epicImages;
        }
    }
}
