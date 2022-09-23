namespace Delega.Api.Models
{
    public class Author : EntityBase
    {
        public Person Person { get; set; }
        public string Depoiment { get; set; }
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
    }
}
