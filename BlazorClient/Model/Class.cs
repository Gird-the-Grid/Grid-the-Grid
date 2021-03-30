using System.ComponentModel.DataAnnotations;

namespace BlazorClient.Model
{ 
    public class LoginModel
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
