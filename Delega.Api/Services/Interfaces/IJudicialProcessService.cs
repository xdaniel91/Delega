using Delega.Api.Models;
using Delega.Api.Models.Requests;
using Delega.Api.Models.ViewModels;

namespace Delega.Api.Services.Interfaces;

public interface IJudicialProcessService
{
    Task<JudicialProcessViewModel> AddAsync(JudicialProcessCreateRequest request);
    Task<JudicialProcessViewModel> GetByIdAsync(int id);
    Task<JudicialProcess> GetWithRelationsAsync(int id);
    Task<IEnumerable<JudicialProcess>> GetAllAsync();
    JudicialProcessViewModel GetViewModel(int id);
    Task<JudicialProcess> InProgressAsync(int id);
    Task<JudicialProcess> CreateNewJudicialProcessAsync(JudicialProcessCreateRequest request);
    Task<Author> CreateNewAuthorAsync(JudicialProcessCreateRequest request);
    Task<Accused> CreateNewAccusedAsync(JudicialProcessCreateRequest request);

}

