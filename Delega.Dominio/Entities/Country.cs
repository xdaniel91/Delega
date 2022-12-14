namespace Delega.Dominio.Entities;

public class Country : EntityBase
{
    public string Name { get; set; }

    public Country(string name)
    {
        Name = name;
    }
}
