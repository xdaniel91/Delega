using Delega.Dominio.Entities;
using FluentValidation;

namespace Delega.Dominio.Validators;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.Street).NotEmpty().Length(3, 90).WithMessage("invalid street");
        RuleFor(x => x.CityId).NotEmpty().WithMessage("city id invalid");
        RuleFor(x => x.District).NotEmpty().WithMessage("invalid district");
    }
}
