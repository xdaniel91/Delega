using Delega.Dominio.Entities;
using Delega.Dominio.Validators;
using FluentAssertions;
using Xunit;

namespace Delega.Tests;

public class InvoiceUnityTests
{
    
    [Fact(DisplayName = "Caulculate value with discount")]
    public void DeveRetornarSucesso()
    {
        /*  inVoice recebe um valor de 100, um bool true indicando se tem desconto, e um decimal (30.4) indicando o percentual do desconto 
         *  a propridade FinalValue verifica o bool que indica se há ou não desconto, se for true FinalValue será = valor - percentual_desconto
         *  logo 100 - 30.4 = 69.6 */

        var invoice = new Invoice(100, true, 30.4);

        Assert.Equal(69.6M, invoice.FinalValue);
    }

    [Fact(DisplayName = "Caulculate value without discount")]
    public void DeveQuebrarNoValidador()
    {
        var invoice = new Invoice(100, false, 90);

        Assert.Equal(100, invoice.FinalValue);
    }

    [Fact(DisplayName = "Invalid discount percentual")]
    public void DeveDarErroNoValorDoDesconto()
    {
        var invoice = new Invoice(100, true, 108);
        var invoiceValidator = new InvoiceValidator();
        var validationResult = invoiceValidator.Validate(invoice);
        var validationMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();

        var contains = validationMessages.Contains("discount invalid");
        Assert.True(contains);
        validationResult.IsValid.Should().BeFalse();
    }
}
