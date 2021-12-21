using Newtonsoft.Json;
using System;

namespace Nasa.Apod.DataAccess.Data.EPIC
{
    public class EPICImage
    {
        [JsonProperty("identifier")]
        public long Id { get; set; }

        public string Caption { get; set; }

        [JsonProperty("image")]
        public string Name { get; set; }

        public string Version { get; set; }

        public DateTime Date { get; set; }

        [JsonProperty("centroid_coordinates")]
        public CentroidCoordinates CentroidCoordinates { get; set; }

        [JsonProperty("dscovr_j2000_position")]
        public DscovrJ2000Position DscovrJ2000Position { get; set; }

        [JsonProperty("lunar_j2000_position")]
        public LunarJ2000Position LunarJ2000Position { get; set; }

        [JsonProperty("sun_j2000_position")]
        public SunJ2000Position SunJ2000Position { get; set; }

        [JsonProperty("attitude_quaternions")]
        public AttitudeQuaternions AttitudeQuaternions { get; set; }

        public Coords Coords { get; set; }

        [JsonIgnore]
        public string ImageData { get; set; }
    }
}
