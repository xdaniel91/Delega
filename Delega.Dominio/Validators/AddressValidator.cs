using Delega.Dominio.Entities;
using FluentValidation;

namespace Delega.Dominio.Validators;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.Street).NotEmpty().Length(3, 90);
        RuleFor(x => x.CityId).NotEmpty();
        RuleFor(x => x.District).NotEmpty();
    }
}
