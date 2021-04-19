using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BlazorServerAPI.Models.Entities
{
    public class CompanyModel : OwnedEntity
    {
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyIdentificationNumber { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public float TaxRates { get; set; }


        public CompanyModel() : base()
        { }

        public CompanyModel(string companyName, string companyIdentificationNumber, string country, float taxRates) : base()
        {
            CompanyName = companyName;
            CompanyIdentificationNumber = companyIdentificationNumber;
            Country = country;
            TaxRates = taxRates;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    
}
