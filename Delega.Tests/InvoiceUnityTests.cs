using Delega.Dominio.Entities;
using Xunit;

namespace Delega.Tests;

public class InvoiceUnityTests
{
    [Fact(DisplayName = "")]
    public async Task DeveRetornarSucesso()
    {
        //Arrange
        var invoice = new Invoice(100, true, 30.4);

        //Assert
        Assert.Equal(69.6M, invoice.FinalValue);
    }

    public async Task DeveQuebrarNoValidador()
    {
        var invoice = new Invoice(100, true, 90);
    }

    [Fact]
    public async Task DeveDarErroNoValorDoDesconto()
    {
        
    }
}
