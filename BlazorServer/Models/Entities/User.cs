﻿using System.Collections.Generic;
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

        public User()
        {
            Email = "test@email.com";
            Password = "testPassword1";
        }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            var x = new System.Net.Mail.MailAddress(Email).ToString();
            if (Email != x)
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
            //TODO: see how to throw error if Admin or other attributes are sent
            //TODO: Add custom messages to password validation
        }
    }
}
