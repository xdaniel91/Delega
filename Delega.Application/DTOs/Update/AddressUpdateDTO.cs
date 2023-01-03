namespace Delega.Infraestrutura.DTOs.Update;

public sealed class AddressUpdateDTO
{
    public long Id { get; set; }
    public string? Street { get; set; }
    public string? District { get; set; }
    public string? ZipCode { get; set; }
    public int? Number { get; set; }
    public string? AdditionalInformation { get; set; }
}
