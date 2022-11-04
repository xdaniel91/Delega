using Delega.Api.Models;
using Delega.Api.Utils;
using FluentValidation;

namespace Delega.Api.Validators;

public class JudicialProcessValidator : AbstractValidator<JudicialProcess>
{
    public JudicialProcessValidator()
    {
        RuleFor(x => x.Accused)
            .NotNull().WithMessage(ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.AccusedNotNull));

        RuleFor(x => x.Author)
            .NotNull().WithMessage(ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.AuthorNotNull));

        RuleFor(x => x.DateHourCreated)
            .GreaterThanOrEqualTo(DateTime.Now.AddDays(-7)).WithMessage(ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.DateHourCreatedInvalid));

        RuleFor(x => x.DateHourInProgress)
            .GreaterThanOrEqualTo(x => x.DateHourCreated).WithMessage(ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.DateHourInProgressInvalid));

        RuleFor(x => x.DateHourFinished)
            .GreaterThanOrEqualTo(x => x.DateHourInProgress).WithMessage(ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.DateHourFinishedInvalid));

        RuleFor(x => x.Lawyer)
            .NotNull().WithMessage(ErrorMessages.GetMessageByLanguageSysid(ErrorMessagesSysid.LawyerNotNull));

    }
}
