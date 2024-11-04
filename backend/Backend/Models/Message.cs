using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API.Models
{
public class Message 
{
        [Newtonsoft.Json.JsonProperty("messageId")]
        public int MessageId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [JsonProperty("date")]
        public System.DateTime Date { get; set; }

        [Required]  
        [StringLength(int.MaxValue)]
        [JsonProperty("message")]
       public string message { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        [JsonProperty("userEmail")]
        public string UserEmail { get; set; }

    }
}