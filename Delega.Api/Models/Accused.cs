namespace Delega.Api.Models
{
    public class Accused : EntityBase
    {
        public Person Person { get; set; }
        public bool Innocent { get; set; }
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        
    }
}
