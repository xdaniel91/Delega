namespace Delega.Dominio.Entities;

public class City : EntityBase
{
    public string Name { get; set; }
    public State State { get; set; }
    public long StateId { get; set; }

    public City()
    {

    }

    public City(string name, long stateId)
    {
        Name = name;
        StateId = stateId;
        CreatedAt = DateTime.UtcNow;
    }
}
