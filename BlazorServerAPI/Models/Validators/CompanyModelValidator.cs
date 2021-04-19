using BlazorServerAPI.Models.Entities;
using FluentValidation;

namespace BlazorServerAPI.Models.Validators
{
    public class CompanyModelValidator : AbstractValidator<CompanyModel>
    {
        public CompanyModelValidator()
        {
            RuleFor(x => x.CompanyName).NotNull().Length(2, 30).WithMessage("Company name must be between 2 and 30 characters. If longer opt for abreviations");
            RuleFor(x => x.CompanyIdentificationNumber).NotNull().Length(2, 30).WithMessage("Company Identification Number is the company unique id");
            RuleFor(x => x.Country).NotNull().Length(2).WithMessage("Country Code must have 2 letters");
            RuleFor(x => x.TaxRates).GreaterThan(0).LessThan(100).WithMessage("Tax Rates must be between 0 and 100");
        }
    }
}
