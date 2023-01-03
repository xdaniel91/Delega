using Delega.Dominio.Entities;
using FluentValidation;

namespace Delega.Dominio.Validators;

public class InvoiceValidator : AbstractValidator<Invoice>
{
    public InvoiceValidator()
    {
        RuleFor(x => x.Discount)
            .NotEmpty()
            .InclusiveBetween(1, 100).WithMessage("discount invalid");

        RuleFor(x => x.Value)
            .GreaterThan(0)
            .NotEmpty()
            .WithMessage("value invalid");
    }
}
