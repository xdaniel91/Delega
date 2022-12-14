using Delega.Dominio.Entities;
using FluentValidation;

namespace Delega.Dominio.Validators;

internal class CountryValidator : AbstractValidator<Country>
{
    public CountryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 90);
    }
}
