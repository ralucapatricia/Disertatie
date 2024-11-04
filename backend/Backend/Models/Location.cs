using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
public class Location
{
        [JsonProperty("locationId")]
        public int LocationId { get; set; }

        [Required]  
        [Range(-90.0, 90.0)]  
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [Required]  
        [Range(-180.0, 180.0)]
        [JsonProperty("lng")]
        public double Lng { get; set; }

        public static Location FromId(int id)
        {
            return new Location { LocationId = id };
        }
    }

}
