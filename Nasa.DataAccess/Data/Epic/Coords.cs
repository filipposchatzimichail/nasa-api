using Newtonsoft.Json;

namespace Nasa.DataAccess.Data.Epic
{
    public class Coords
    {
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
    }
}
