using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorServerAPI.Models.Entities
{
    //TODO: add atributes needed
    public class GridModel : BaseEntity, IValidatableObject
    {

        public GridModel() : base()
        {
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //TODO: add custom validation if needed, otherwise remove
            return null;
        }
    }

}
