using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API.Models
{
    public class Place
    {
        [JsonProperty("placeId")]
        public int PlaceId { get; set; }

        [Required]  
        [StringLength(255)]  
        [JsonProperty("address")]
        public string Address { get; set; }

        [Required] 
        [DataType(DataType.Date)]  
        [JsonProperty("date")]  
        public System.DateTime Date { get; set; }

        [StringLength(int.MaxValue)]
        [JsonProperty("description")]
        public string Description { get; set; }

        [StringLength(255)]
        [JsonProperty("imageUriC")]
        public string ImageUriC { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [Required]
        [JsonProperty("title")]
        public string Title { get; set; }

        [Required]  
        [EmailAddress]  
        [JsonProperty("userId")]
        public int UserId { get; set; }
    }
}
