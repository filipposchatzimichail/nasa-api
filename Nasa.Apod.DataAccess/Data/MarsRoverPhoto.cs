using Newtonsoft.Json;
using System;

namespace Nasa.Apod.DataAccess.Data
{
    public class MarsRoverPhoto
    {
        public int Id { get; set; }

        [JsonProperty("sol")]
        public int MartianDay { get; set; }
        public Camera Camera { get; set; }

        [JsonProperty("img_src")]
        public string Url { get; set; }

        [JsonProperty("earth_date")]
        public DateTime EarthDate { get; set; }
        public Rover Rover { get; set; }
    }
}
