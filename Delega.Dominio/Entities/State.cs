namespace Delega.Dominio.Entities;

public class State : EntityBase
{
    public string Name { get; set; }
    public Country Country { get; set; }
    public long CountryId { get; set; }

    public State(string name, long countryId)
    {
        Name = name;
        CountryId = countryId;
    }

    public State()
    {

    }
}
