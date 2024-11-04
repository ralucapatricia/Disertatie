using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API.Authentication.Models
{
    public class SignInResponse
    {
        [JsonProperty("tokenId")]
        public string TokenId { get; set; }

        [Required]
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [Required]
        [JsonProperty("token")]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [JsonProperty("expirationDate")]
        public DateTime ExpirationDate { get; set; }

    }
}
