using BlazorServerAPI.Utils.Exceptions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorServerAPI.Models.Entities
{
    public class User : BaseEntity, IValidatableObject
    {
        

        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        public string Password { get; set; }

        public bool Admin { get; set; }

        public bool Activated { get; set; }

        public User()
        {
            Email = "test@email.com";
            Password = "testPassword1";
        }

        public User(string email, string password) : base()
        {
            Email = email;
            Password = password;
            Admin = false;
            Activated = false;
        }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            var emailOk = true;
            try
            {
                var x = new System.Net.Mail.MailAddress(Email).ToString();
                if (Email != x)
                {
                    emailOk = false;
                }
            } catch (InvalidEmailException)
            {
                emailOk = false;
            }
            if (!emailOk)
            {
                yield return new ValidationResult(
                       $"Email not valid."
                   );
            }
            
            
            if (Admin == true)
            {
                yield return new ValidationResult(
                    $"Illegal argument \"Admin\"."
                );
            }
            //TODO: note[Ana]: maybe a class UserException(emailOk,passwordOk,adminOk) that throws Exceptions based of each atribute value
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
