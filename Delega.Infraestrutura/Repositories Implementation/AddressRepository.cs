using Delega.Application.Exceptions;
using Delega.Dominio.Entities;
using Delega.Infraestrutura.Database;
using Delega.Infraestrutura.Repositories_Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Delega.Infraestrutura.Repositories_Implementation;

public class AddressRepository : IAddressRepository
{
    protected readonly DelegaContext Context;
    protected readonly DbSet<Address> Addresses;

    public AddressRepository(DelegaContext dbContext)
    {
        Context = dbContext;
        Addresses = Context.address;
    }

    public async Task<Address> AddAddressAsync(Address address, CancellationToken cancellationToken)
    {
        try
        {
            var result = await Addresses.AddAsync(address, cancellationToken);
            return result.Entity;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<Address> GetAddressAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await Addresses.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            return result is null ? throw new DelegaDataException("Address not found") : result;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<Address> UpdateAddressAsync(Address address, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            throw new DelegaDataException("Request cancelled");

        try
        {
            var result = Addresses.Update(address);

            return result.Entity;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
