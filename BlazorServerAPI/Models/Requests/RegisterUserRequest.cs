using System.ComponentModel.DataAnnotations;

namespace BlazorServerAPI.Models.Requests
{
    //TODO: to delete if not needed anymore
    public class RegisterUserRequest 
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
