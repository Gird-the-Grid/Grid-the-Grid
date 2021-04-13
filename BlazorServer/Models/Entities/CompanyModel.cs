using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

        public CompanyModel(string companyName, string cin, string country, float taxRates) : base()
        {
            CompanyName = companyName;
            CompanyIdentificationNumber = cin;
            Country = country;
            TaxRates = taxRates;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //TODO: add custom validation if needed, otherwise remove
            return null;
        }
    }
}
