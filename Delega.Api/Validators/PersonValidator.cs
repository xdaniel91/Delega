using Delega.Api.Models;
using Delega.Api.Utils;
using FluentValidation;

namespace Delega.Api.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator(Dictionary<string, string> messages)
        {
            RuleFor(X => X.FirstName)
                .NotNull().WithMessage(messages[ConsMessagesSysId.FirstNameNotNullSysid])
                .NotEmpty().WithMessage(messages[ConsMessagesSysId.FirstNameNotEmptySysid])
                .MinimumLength(3).WithMessage(messages[ConsMessagesSysId.FirstNameMinimiumLengthSysid]);

            RuleFor(X => X.LastName)
                .NotNull().WithMessage(messages[ConsMessagesSysId.LastNameNotNullSysid])
                .NotEmpty().WithMessage(messages[ConsMessagesSysId.LastNameNotEmptySysid])
                .MinimumLength(3).WithMessage(messages[ConsMessagesSysId.LastNameMinimiumLengthSysid]);

            RuleFor(X => X.Cpf)
                .NotNull()
                .NotEmpty().WithMessage(messages[ConsMessagesSysId.CpfNotEmptySysid])
                .Length(11, 11);

            RuleFor(X => X.BirthDate).NotNull().NotEmpty()
                .LessThan(DateTime.Today.AddYears(-18))
                .GreaterThan(DateTime.Today.AddYears(-110));
        }
    }
}
