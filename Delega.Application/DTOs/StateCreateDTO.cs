using System.ComponentModel.DataAnnotations;

namespace Delega.Infraestrutura.DTOs;

public sealed class StateCreateDTO
{
    [Required]
    public string Name { get; set; }
    [Required]
    public long CountryId { get; set; }
}
