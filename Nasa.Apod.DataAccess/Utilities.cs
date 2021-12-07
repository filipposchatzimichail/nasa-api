﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ApodData = Nasa.Apod.DataAccess.Data.Apod;
using MarsRoverPhotoData = Nasa.Apod.DataAccess.Data.MarsRoverPhoto;

namespace Nasa.Apod.DataAccess
{
    public static class Utilities
    {
        public static ApodData GetApodFromJson(string json)
        {
            return JsonConvert.DeserializeObject<ApodData>(json);
        }

        public static List<MarsRoverPhotoData> GetMarsRoverPhotoFromJson(string json)
        {
            var photos = JObject.Parse(json).SelectToken("photos").ToString();

            var what = JsonConvert.DeserializeObject<List<MarsRoverPhotoData>>(photos);

            return what;
        }
    }
}
