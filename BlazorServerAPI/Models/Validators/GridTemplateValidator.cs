using BlazorServerAPI.Models.Entities;
using FluentValidation;

namespace BlazorServerAPI.Models.Validators
{
    public class GridTemplateValidator : AbstractValidator<GridTemplate>
    {
        public GridTemplateValidator()
        {
            RuleFor(gridTemplate => gridTemplate.Vertexes).InclusiveBetween(2, 1900000).WithMessage("Clients on greed must be between 2 and 1900000");
            RuleFor(gridTemplate => gridTemplate.Edges).GreaterThan(1).Must((gridTemplate, edges) => {
                return gridTemplate.Vertexes * (gridTemplate.Vertexes - 1) / 4 > edges;
            }).WithMessage(gridTemplate => $"Connections on greed must be between 1 and {gridTemplate.Vertexes * (gridTemplate.Vertexes - 1) / 4 - 1}");
        }
    }
}
