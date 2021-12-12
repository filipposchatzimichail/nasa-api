using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.Apod.DataAccess.Data
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
