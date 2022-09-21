using Delega.Api.Models;
using Delega.Api.Models.Requests;

namespace Delega.Api.Interfaces.Services;

public interface IJudicialProcessService
{
    JudicialProcess Add(JudicialProcessCreateRequest request);
    JudicialProcess GetByIdWithRelationships(int id);
    IEnumerable<JudicialProcess> GetAllWithRelationships();
}

