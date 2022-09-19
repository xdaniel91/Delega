using Delega.Api.Models;
using Delega.Api.Utils;
using FluentValidation;

namespace Delega.Api.Validators
{
    public class JudicialProcessValidator : AbstractValidator<JudicialProcess>
    {
        public JudicialProcessValidator(Dictionary<string, string> messages)
        {
            RuleFor(x => x.Accused)
                .NotNull().WithMessage(messages[ConsMessagesSysId.AccusedNotNull]);
           
            RuleFor(x => x.Author)
                .NotNull().WithMessage(messages[ConsMessagesSysId.AuthorNotNull]);

            RuleFor(x => x.DateHourCreated)
                .Equal(DateTime.Today).WithMessage(messages[ConsMessagesSysId.DateHourCreatedInvalid]);

            RuleFor(x => x.DateHourInProgress)
                .GreaterThanOrEqualTo(x => x.DateHourCreated).WithMessage(messages[ConsMessagesSysId.DateHourInProgressInvalid]);

            RuleFor(x => x.DateHourFinished)
                .GreaterThanOrEqualTo(x => x.DateHourInProgress).WithMessage(messages[ConsMessagesSysId.DateHourFinishedInvalid]);

            RuleFor(x => x.Lawyer)
                .NotNull().WithMessage(messages[ConsMessagesSysId.LawyerNotNull]);

        }
    }
}
