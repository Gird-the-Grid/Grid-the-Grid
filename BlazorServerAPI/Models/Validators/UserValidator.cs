using BlazorServerAPI.Models.Entities;
using FluentValidation;

namespace BlazorServerAPI.Models.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(user => $"{user.Email} is not a valid mail address");
            RuleFor(user => user.Password).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$").WithMessage("Password must have at least 1 capital letter and a number and must be between 8 and 15 characters");
            //RuleFor(user => user.Admin).Equal(true).WithMessage("Malicious client tries to have admin. There's no such property"); //TODO: this rule seems to always trigger, amend this when inserting
        }
    }
}
