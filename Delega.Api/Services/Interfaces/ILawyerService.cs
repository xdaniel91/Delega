using Delega.Api.Models;

namespace Delega.Api.Services.Interfaces;

public interface ILawyerService
    {
        Task<Lawyer> AddAsync(LawyerCreateRequest lawyer);
        Task<Lawyer> GetByIdAsync(int id);
        IEnumerable<Lawyer> GetAll();
        LawyerResponse GetResponse(int id);
    }

