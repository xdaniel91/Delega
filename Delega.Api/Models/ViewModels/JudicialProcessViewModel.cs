using static Delega.Api.Utils.ConstGeneral;

namespace Delega.Api.Models.ViewModels
{
    public class JudicialProcessViewModel
    {
        public int Id { get; set; }
        public string AccusedName { get; set; }
        public int AccusedId { get; set; }
        public string AuthorName { get; set; }
        public int AuthorId { get; set; }
        public int MyProperty { get; set; }
        public int LawyerId { get; set; }
        public decimal RequestedValue { get; set; }
        public decimal? Value { get; set; }
        public string Reason { get; set; }
        public string? Verdict { get; set; }
        public StatusJudicialProcess Status { get; set; }
        public string TreatedStatus
        {
            get
            {
                return Status switch
                {
                    StatusJudicialProcess.Created => JudicialProcessStatusCreated.Description,
                    StatusJudicialProcess.InProgress => JudicialProcessStatusInProgress.Description,
                    StatusJudicialProcess.Finished => JudicialProcessStatusFinished.Description,
                    _ => string.Empty
                };
            }
        }
        public DateTime DateHourCreated { get; set; }
        public DateTime? DateHourInProgress { get; set; }
        public DateTime? DateHourFinished { get; set; }
    }
}
