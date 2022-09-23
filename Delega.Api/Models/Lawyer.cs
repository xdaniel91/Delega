namespace Delega.Api.Models
{
    public class Lawyer : EntityBase
    {
        public Person Person { get; set; }
        public string Oab { get; set; }
        public string Name { get; set; }
        public int PersonId { get; set; }
    }
}
