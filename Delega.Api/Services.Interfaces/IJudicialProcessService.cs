using Delega.Api.Models;
using Delega.Api.Models.Requests;

namespace Delega.Api.Services.Interfaces
{
    public interface IJudicialProcessService
    {
        JudicialProcess Add(JudicialProcessCreateRequest request);
        JudicialProcess GetByIdWithRelationships(int id);
        IEnumerable<JudicialProcess> GetAllWithRelationships();
    }
}
