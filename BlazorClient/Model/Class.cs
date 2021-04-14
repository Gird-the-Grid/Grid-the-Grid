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

    public interface IBaseModel
    {
        string Id { get; set; }
        string CreatedAt { get; set; }
        string UpdatedAt { get; set; }
    }

    public class BaseModel : IBaseModel
    {
        public string Id { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }

    public class CompanyModel : BaseModel
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string CompanyIdentificationNumber { get; set; }

        [Required]
        public string Country { get; set; } //TODO: add validation max length 2 letters

        [Required] //TODO: add validation 0 < t < 100
        public float TaxRates { get; set; }

        public string OwnerId { get; set; }
    }
}
