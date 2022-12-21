using Delega.Dominio.Entities;
using FluentValidation;

namespace Delega.Dominio.Validators;

public class InvoiceValidator : AbstractValidator<Invoice>
{
    public InvoiceValidator()
    {
        RuleFor(x => x.Discount)
            .NotEmpty()
            .GreaterThan(0.0);

        RuleFor(x => x.Value)
            .NotEmpty();
    }
}
