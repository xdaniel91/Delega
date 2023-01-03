using Delega.Dominio.Entities;
using FluentValidation;

namespace Delega.Dominio.Validators;

public class CountryValidator : AbstractValidator<Country>
{
    public CountryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("name invalid");
    }
}