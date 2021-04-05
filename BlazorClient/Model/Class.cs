using System.ComponentModel.DataAnnotations;

namespace BlazorClient.Model
{ 
    public class LoginModel
    {
        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Username does not appear to be a valid email adress")]
        public string username { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "Password must contain at least 8 characters, one capital letter and one number")]
        public string password { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Username does not appear to be a valid email adress")]
        public string username { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "Password must contain at least 8 characters, one capital letter and one number")]
        public string password1 { get; set; }

        [Required]
        [CompareProperty("password1", ErrorMessage = "Passwords must match !")]
        public string password2 { get; set; }
    }
}
