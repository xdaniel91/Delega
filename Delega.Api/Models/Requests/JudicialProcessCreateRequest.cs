namespace Delega.Api.Models.Requests
{
    public class JudicialProcessCreateRequest
    {
        public int AuthorId { get; set; }
        public int AccusedId { get; set; }
        public int LawyerId { get; set; }
        public decimal RequestedValue { get; set; }
        public string Reason { get; set; }
        public string AuthorDepoiment { get; set; }
    }
}