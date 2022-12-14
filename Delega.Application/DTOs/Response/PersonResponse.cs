namespace Delega.Infraestrutura.DTOs.Response;

public sealed class PersonResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Cpf { get; set; }
    public DateTime BirthDate { get; set; }
    public int Age {
        get {
            var age = DateTime.Today.Year - BirthDate.Year;
            if (BirthDate < DateTime.Today)
                age--;

            return age;
        }
    }
    public long AddressId { get; set; }
}
