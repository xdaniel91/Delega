namespace Delega.Api.Interfaces
{
    public interface IConsMessages
    {
        void SetMessages(string language);
        Dictionary<string, string> GetMessages();

    }
}
