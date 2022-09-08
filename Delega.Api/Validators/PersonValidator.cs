using Delega.Api.Models;
using FluentValidation;

namespace Delega.Api.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(X => X.FirstName).NotNull().NotEmpty().MinimumLength(3).WithMessage("Invalid first name.");
            RuleFor(X => X.LastName).NotNull().NotEmpty().MinimumLength(3).WithMessage("Invalid last name.");
            RuleFor(X => X.Cpf).NotNull().NotEmpty().Length(11, 11).WithMessage("Invalid Cpf.");
            RuleFor(X => X.BirthDate).NotNull().NotEmpty()
                .LessThan(DateTime.Today.AddYears(-18))
                .GreaterThan(DateTime.Today.AddYears(-110))
                .WithMessage("Person must have between 18 and 110 years old.");
        }
    }
}
