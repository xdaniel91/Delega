using Delega.Dominio.Entities;
using FluentValidation;

namespace Delega.Dominio.Validators;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.Street)
            .NotNull().WithMessage("invalid street")
            .NotEmpty().WithMessage("invalid street")
            .Length(3, 90).WithMessage("invalid street");
        
        RuleFor(x => x.ZipCode)
            .NotNull().WithMessage("invalid zip code")
            .NotEmpty().WithMessage("invalid zip code")
            .Length(3, 90).WithMessage("invalid zip code");
       
        RuleFor(x => x.CityId)
            .GreaterThan(0).WithMessage("invalid city id")
            .NotEmpty().WithMessage("invalid city id");
        
        RuleFor(x => x.District)
            .NotNull().WithMessage("invalid district")
            .NotEmpty().WithMessage("invalid district");

    }
}
