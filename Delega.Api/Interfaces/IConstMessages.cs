namespace Delega.Api.Interfaces
{
    public interface IConstMessages
    {
        void SetMessages(string language);
        Dictionary<string, string> GetMessages();

    }
}
