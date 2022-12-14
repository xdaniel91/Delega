using Delega.Dominio.Entities;
using FluentValidation;

namespace Delega.Infraestrutura.Services_Implementation;

public class CountryValidator : AbstractValidator<Country>
{
    public CountryValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}