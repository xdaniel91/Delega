using System.ComponentModel.DataAnnotations.Schema;

namespace Delega.Dominio.Entities;

public class EntityBase
{
    [Column("id")]
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
}
