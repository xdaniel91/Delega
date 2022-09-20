using Delega.Api.Models;
using Delega.Api.Models.Requests;
using Delega.Api.Models.ViewModels;

namespace Delega.Api.Repositories.Interfaces
{
    public interface IJudicialProcessRepository
    {
        JudicialProcess Add(JudicialProcess JudicialProcess);
       // JudicialProcess GetWithRelationships(int id);
        IEnumerable<JudicialProcess> GetAllWithRelationships();
        IEnumerable<JudicialProcessViewModel> GetAll();
    }
}
