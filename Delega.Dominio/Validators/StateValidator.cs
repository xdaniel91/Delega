using Delega.Dominio.Entities;
using FluentValidation;

namespace Delega.Dominio.Validators;

public class StateValidator : AbstractValidator<State>
{
    public StateValidator()
    {
        RuleFor(x => x.CountryId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().Length(3, 90);
    }
}
