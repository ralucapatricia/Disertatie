using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API.Models
{
    public class SignInRequest
    {
        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
