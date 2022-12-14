using Delega.Dominio.Entities;
using FluentValidation;

namespace Delega.Dominio.Validators;

public class CityValidator : AbstractValidator<City>
{
    public CityValidator()
    {
        RuleFor(x => x.StateId)
            .NotEmpty();
       
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 20);
    }
}


