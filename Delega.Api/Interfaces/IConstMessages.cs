namespace Delega.Api.Interfaces
{
    public interface IConstMessages
    {

        Dictionary<string, string> GetMessages(string language = "pt-br");
    }
}
