using Delega.Api.Models;
using Delega.Api.Utils;
using FluentValidation;

namespace Delega.Api.Validators;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(X => X.FirstName)
            .NotNull().WithMessage(ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.FirstNameNotNullSysid))
            .NotEmpty().WithMessage(ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.FirstNameNotEmptySysid))
            .MinimumLength(3).WithMessage(ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.FirstNameMinimiumLengthSysid));

        RuleFor(X => X.LastName)
            .NotNull().WithMessage(ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.LastNameNotNullSysid))
            .NotEmpty().WithMessage(ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.LastNameNotEmptySysid))
            .MinimumLength(3).WithMessage(ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.LastNameMinimiumLengthSysid));

        RuleFor(X => X.Cpf)
            .NotNull()
            .NotEmpty().WithMessage(ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.CpfNotEmptySysid))
            .Length(11, 11);

        RuleFor(X => X.BirthDate)
            .NotNull().WithMessage(ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.BirthDateInvalidSysid))
            .NotEmpty().WithMessage(ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.BirthDateInvalidSysid))
            .LessThan(DateTime.Today.AddYears(-18)).WithMessage(ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.BirthDateInvalidSysid))
            .GreaterThan(DateTime.Today.AddYears(-110)).WithMessage(ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.BirthDateInvalidSysid));
    }
}
