using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Response;
using Delega.Infraestrutura.DTOs.Update;

namespace Delega.Infraestrutura.Services_Interfaces;

public interface IAddressService
{
    Task<AddressResponse> AddAddressAsync(AddressCreateDTO addressCad, CancellationToken cancellationToken);
    Task<AddressResponse> UpdateAddressAsync(AddressUpdateDTO addressUpdate, CancellationToken cancellationToken);
    Task<AddressResponse> GetAddressAsync(long id, CancellationToken cancellationToken);
}
