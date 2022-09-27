using Delega.Api.Models;
using Delega.Api.Models.ViewModels;

namespace Delega.Api.Interfaces.Repositories;

public interface IJudicialProcessRepository
{
    Task<JudicialProcess> AddAsync(JudicialProcess JudicialProcess);
    Task<JudicialProcessViewModel> GetByIdAsync(int id);
    Task<IEnumerable<JudicialProcess>> GetAllAsync();
    JudicialProcessViewModel GetResponse(int id);
    Task<JudicialProcess> GetWithRelationsAsync(int id);
    JudicialProcess Update(JudicialProcess judicialProcess);
}

