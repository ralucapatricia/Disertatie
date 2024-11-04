using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API.Authentication.Models
{
    public class SignInRequest
    {
        [Key]
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [Required]
        [JsonProperty("userEmail")]
        public string UserEmail { get; set; }

        [Required]
        [JsonProperty("passwordHash")]
        public string PasswordHash { get; set; }
    }
}
