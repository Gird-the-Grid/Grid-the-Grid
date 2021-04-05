using System.ComponentModel.DataAnnotations;

namespace BlazorServerAPI.Models.Requests
{
    //TODO: to delete
    public class RegisterUser 
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
