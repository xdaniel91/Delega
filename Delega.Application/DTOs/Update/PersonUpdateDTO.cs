using System.ComponentModel.DataAnnotations;

namespace Delega.Infraestrutura.DTOs.Update;

public sealed class PersonUpdateDTO
{
    [Required]
    public long Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Cpf { get; set; }
    public DateTime? BirthDate { get; set; }
}
