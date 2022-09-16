namespace Delega.Api.Utils
{
    public class DelegaConst
    {
        public string Sysid { get; set; }
        public string Description { get; set; }
    }

    public static class ConsGeneral
    {
        public static readonly DelegaConst JudicialProcessStatusCreated = new()
        {
            Sysid = "Created",
            Description = "Created"
        };

        public static readonly DelegaConst JudicialProcessStatusInProgress = new()
        {
            Sysid = "InProgress",
            Description = "In Progress"
        };

        public static readonly DelegaConst JudicialProcessStatusFinished = new()
        {
            Sysid = "Finished",
            Description = "Finished"
        };

        public static IEnumerable<DelegaConst> JudicialProcessStatus()
        {
            yield return JudicialProcessStatusCreated;
            yield return JudicialProcessStatusInProgress;
            yield return JudicialProcessStatusFinished;
        }
    }
}
