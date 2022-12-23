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
            .MinimumLength(3).WithMessage("firstname invalid");

        RuleFor(X => X.LastName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3).WithMessage("lastname invalid");

        RuleFor(X => X.Cpf)
            .NotNull()
            .NotEmpty()
            .Length(11, 11).WithMessage("cpf invalid");

        RuleFor(X => X.BirthDate)
            .NotNull()
            .NotEmpty()
            .LessThan(DateTime.Today.AddYears(-18))
            .GreaterThan(DateTime.Today.AddYears(-110)).WithMessage("birthdate invalid");
    }
}
