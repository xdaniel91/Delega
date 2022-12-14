using Delega.Dominio.Entities;
using FluentValidation;

namespace Delega.Dominio.Validators;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(X => X.FirstName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(X => X.LastName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(X => X.Cpf)
            .NotNull()
            .NotEmpty()
            .Length(11, 11);

        RuleFor(X => X.BirthDate)
            .NotNull()
            .NotEmpty()
            .LessThan(DateTime.Today.AddYears(-18))
            .GreaterThan(DateTime.Today.AddYears(-110));
    }
}
