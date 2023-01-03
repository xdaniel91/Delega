using System.ComponentModel.DataAnnotations;

namespace Delega.Infraestrutura.DTOs;

public sealed class AddressCreateDTO
{
    [Required]
    public string Street { get; set; }
    [Required]
    public string District { get; set; }
    [Required]
    public string ZipCode { get; set; }
    public int? Number { get; set; }
    public string? AdditionalInformation { get; set; }
    [Required]
    public long CityId { get; set; }
}
