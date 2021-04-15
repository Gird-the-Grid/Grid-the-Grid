using System.ComponentModel.DataAnnotations;

namespace BlazorClient.Model.Entities
{
    public class CompanyModel : BaseEntity
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string CompanyIdentificationNumber { get; set; }

        [StringLength(2, MinimumLength = 1)]
        [Required]
        public string Country { get; set; } 

        [Range(1, 100)]
        [Required]
        public float TaxRates { get; set; }

        public string OwnerId { get; set; }
    }
}
