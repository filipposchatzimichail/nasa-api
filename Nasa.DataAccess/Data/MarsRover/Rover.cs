using Newtonsoft.Json;
using System;

namespace Nasa.DataAccess.Data.MarsRover
{
    public class Rover
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [JsonProperty("landing_date")]
        public DateTime LandingDate { get; set; }

        [JsonProperty("launch_date")]
        public DateTime LaunchDate { get; set; }
        public string Status { get; set; }
    }
}
