using Delega.Api.Models;

namespace Delega.Api.Interfaces.Services
{
    public interface ILawyerService
    {
        LawyerResponse Add(LawyerCreateRequest lawyer);
        Lawyer GetById(int id);
        IEnumerable<Lawyer> GetAll();
        LawyerResponse GetResponse(int id);
    }
}
