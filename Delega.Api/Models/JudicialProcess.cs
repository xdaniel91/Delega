using Delega.Api.Utils;
using static Delega.Api.Utils.ConstGeneral;

namespace Delega.Api.Models
{
    public class JudicialProcess
    {
        public int Id { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public Accused Accused { get; set; }
        public int AccusedId { get; set; }
        public Lawyer Lawyer { get; set; }
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
                    StatusJudicialProcess.Created => ConstGeneral.JudicialProcessStatusCreated.Description,
                    StatusJudicialProcess.InProgress => ConstGeneral.JudicialProcessStatusInProgress.Description,
                    StatusJudicialProcess.Finished => ConstGeneral.JudicialProcessStatusFinished.Description,
                    _ => string.Empty
                };
            }
        }

        public DateTime DateHourCreated { get; set; }
        public DateTime? DateHourInProgress { get; set; }
        public DateTime? DateHourFinished { get; set; }
    }
}