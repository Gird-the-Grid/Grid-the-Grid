using System.ComponentModel.DataAnnotations;

namespace BlazorClient.Model.Entities
{
    public class CompanyModel : BaseEntity
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
