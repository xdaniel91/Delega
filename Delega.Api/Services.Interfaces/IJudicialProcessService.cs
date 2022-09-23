using Delega.Api.Models;
using Delega.Api.Models.Requests;
using Delega.Api.Models.ViewModels;

namespace Delega.Api.Interfaces.Services;

public interface IJudicialProcessService
{
    JudicialProcessViewModel Add(JudicialProcessCreateRequest request);
    JudicialProcess GetByIdWithRelationships(int id);
    IEnumerable<JudicialProcess> GetAllWithRelationships();
    JudicialProcessViewModel GetById(int id);
}

