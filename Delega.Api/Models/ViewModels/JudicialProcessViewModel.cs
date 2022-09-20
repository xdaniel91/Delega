namespace Delega.Api.Models.ViewModels
{
    public class JudicialProcessViewModel
    {
        public string AuthorName { get; set; }
        public string AccusedName{ get; set; }
        public string LawyerName { get; set; }
        public decimal RequestedValue { get; set; }
        public decimal Value { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public DateTime DateHourCreated { get; set; }
        public DateTime? DateHourInProgress { get; set; }
        public DateTime? DateHourFinished { get; set; }
    }
}
