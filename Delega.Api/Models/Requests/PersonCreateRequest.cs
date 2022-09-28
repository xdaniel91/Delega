using System.ComponentModel.DataAnnotations;

namespace Delega.Api.Models
{
    public class PersonCreateRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
