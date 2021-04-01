using System.ComponentModel.DataAnnotations;

namespace BlazorServerAPI.Models.Requests
{
    public class RegisterUser 
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Password2 { get; set; }
    }
}
