using Delega.Api.Models;

namespace Delega.Api.Interfaces.Repositories;

public interface ILawyerRepository
{
    Lawyer Add(Lawyer lawyer);
    Task<Lawyer> GetByIdAsync(int id);
    IEnumerable<Lawyer> GetAll();
    LawyerResponse GetResponse(int id);
}

