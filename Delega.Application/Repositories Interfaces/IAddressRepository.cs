using Delega.Dominio.Entities;

namespace Delega.Infraestrutura.Repositories_Interfaces;

public interface IAddressRepository
{
    Task<Address> AddAddressAsync(Address address, CancellationToken cancellationToken);
    Task<Address> UpdateAddressAsync(Address address, CancellationToken cancellationToken);
    Task<Address> GetAddressAsync(long id, CancellationToken cancellationToken);
}
