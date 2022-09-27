using static Delega.Api.Utils.ConstGeneral;

namespace Delega.Api.Models.Requests;

public class JudicialProcessInProgressRequest
{
    public int Id { get; set; }
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
}
