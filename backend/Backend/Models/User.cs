using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API.Models
{
    public class User
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
        
        [Required]

        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("passwordHash")]
        public string PasswordHash { get; set; }
    }
}
