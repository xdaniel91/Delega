using Delega.Api.Utils;

namespace Delega.Api.Models
{
    public class JudicialProcess
    {
        public Person Author { get; set; }
        public Person Accused { get; set; }
        public Lawyer Lawyer { get; set; }
        public decimal RequestedValue { get; set; }
        public decimal? Value { get; set; }
        public string Reason { get; set; }
        public string? Verdict { get; set; }
        public string Status { get; set; }
        public string TreatedStatus
        {
            get
            {
                return Status switch
                {
                    "0" => ConsGeneral.JudicialProcessStatusCreated.Description,
                    "1" => ConsGeneral.JudicialProcessStatusInProgress.Description,
                    "2" => ConsGeneral.JudicialProcessStatusFinished.Description,
                    _ => string.Empty
                };
            }
        }

        public DateTime DateHourCreated { get; set; }
        public DateTime? DateHourInProgress { get; set; }
        public DateTime? DateHourFinished { get; set; }
    }
}