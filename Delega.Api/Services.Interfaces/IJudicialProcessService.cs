using Delega.Api.Models;
using Delega.Api.Models.Requests;
using Delega.Api.Models.ViewModels;

namespace Delega.Api.Interfaces.Services;

public interface IJudicialProcessService
{
    Task<JudicialProcessViewModel> AddAsync(JudicialProcessCreateRequest request);
    Task<JudicialProcessViewModel> GetByIdAsync(int id);
    Task<JudicialProcess> GetWithRelationsAsync(int id);
    JudicialProcessViewModel GetViewModel(int id);
}

