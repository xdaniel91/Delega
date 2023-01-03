using System.ComponentModel.DataAnnotations;

namespace Delega.Infraestrutura.DTOs.Update;

public sealed class StateUpdateDTO
{
    [Required]
    public long Id { get; set; }
    public long? CountryId { get; set; }
    public string? Name { get; set; }
}
