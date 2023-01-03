namespace Delega.Infraestrutura.DTOs.Response;

public sealed class AddressResponse
{
    public string Street { get; set; }
    public string District { get; set; }
    public string ZipCode { get; set; }
    public int? Number { get; set; }
    public string? AdditionalInformation { get; set; }
    public string City { get; set; }
}
