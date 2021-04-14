using BlazorServerAPI.Utils.Exceptions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorServerAPI.Models.Entities
{
    public class CompanyModel : BaseEntity, IValidatableObject
    {
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyIdentificationNumber { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public float TaxRates { get; set; }
        public string OwnerId { get; set; }

        public CompanyModel() : base()
        { }

        public CompanyModel(string companyName, string companyIdentificationNumber, string country, float taxRates) : base()
        {
            CompanyName = companyName;
            CompanyIdentificationNumber = companyIdentificationNumber;
            Country = country;
            TaxRates = taxRates;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //TODO: [Required] not working for TaxRates field, to be verrified here or see if FluentValidation solves this
            if (Country.Length != 2)
            {
                yield return new ValidationResult(
                       $"Country has to have 2 letters"
                );
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
