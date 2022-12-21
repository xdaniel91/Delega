using Delega.Dominio.Exceptions;
using Delega.Dominio.Validators;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delega.Dominio.Entities;

[NotMapped]
public class Invoice
{
    public decimal Value { get; private  set; }
    public bool HaveDiscount { get; private  set; }
    public double Discount { get; private  set; }

    public decimal FinalValue {
        get 
        {
            if (HaveDiscount)
            {
                double percentualDesconto = Discount / 100.0;
                var valorFinal = Value - ((decimal)percentualDesconto * Value);
                return valorFinal;
            }
            
            return Value;
        }
    }

    public Person Person { get; private set; }

    public Invoice()
    {

    }

    public Invoice(decimal valor, bool temDesconto, double percentualDesconto)
    {
        Value = valor;
        Discount = percentualDesconto;
        HaveDiscount = temDesconto;
    }

    private async Task<bool> ValidateAsync(Invoice invoice, CancellationToken cancellationToken)
    {
        var _invoiceValidator = new InvoiceValidator();

        var validationResult = await _invoiceValidator.ValidateAsync(invoice, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
            var errorsString = string.Join(",", errors);
            throw new DelegaDomainException(errorsString);
        }

        return true;
    }

    public async void Update(bool? haveDiscount, double? discount, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            throw new OperationCanceledException("Operação cancelada");

        try
        {
            if (haveDiscount is not null)
                HaveDiscount = haveDiscount.Value;

            if (discount is not null)
                Discount = discount.Value;

            await ValidateAsync(this, cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
