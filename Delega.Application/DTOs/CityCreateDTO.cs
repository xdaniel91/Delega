using System.ComponentModel.DataAnnotations;

namespace Delega.Infraestrutura.DTOs;

public sealed class CityCreateDTO
{
    [Required]
    public string Name { get; set; }
    [Required]
    public long StateId { get; set; }
}
