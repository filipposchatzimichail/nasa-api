using Newtonsoft.Json;

namespace Nasa.DataAccess.Data.MarsRover
{
    public class Camera
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [JsonProperty("rover_id")]
        public int RoverId { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }
    }
}
