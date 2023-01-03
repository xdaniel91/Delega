using System.ComponentModel.DataAnnotations;

namespace Delega.Infraestrutura.DTOs;

public class PersonCreateDTO
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Cpf { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    public long AddressId { get; set; }
}
