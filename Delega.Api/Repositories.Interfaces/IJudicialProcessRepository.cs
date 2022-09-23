using Delega.Api.Models;
using Delega.Api.Models.ViewModels;

namespace Delega.Api.Interfaces.Repositories;

public interface IJudicialProcessRepository
{
    Task<JudicialProcessViewModel> AddAsync(JudicialProcess JudicialProcess);
    IEnumerable<JudicialProcess> GetAllWithRelationships();
    JudicialProcessViewModel GetById(int id);
}

