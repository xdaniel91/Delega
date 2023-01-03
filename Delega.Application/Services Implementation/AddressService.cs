using Delega.Application.Repositories_Interfaces;
using Delega.Dominio.Factories;
using Delega.Dominio.Validators;
using Delega.Infraestrutura.DTOs;
using Delega.Infraestrutura.DTOs.Response;
using Delega.Infraestrutura.DTOs.Update;
using Delega.Infraestrutura.Repositories_Interfaces;
using Delega.Infraestrutura.Services_Interfaces;

namespace Delega.Infraestrutura.Services_Implementation;

public class AddressService : IAddressService
{
    protected readonly IAddressRepository _addressRepository;
    protected readonly IUow _uow;
    protected readonly AddressValidator _addressvalidator = new AddressValidator();

    public AddressService(IAddressRepository addressRepository, IUow uow)
    {
        _addressRepository = addressRepository;
        _uow = uow;
    }

    public async Task<AddressResponse> AddAddressAsync(AddressCreateDTO addressCad, CancellationToken cancellationToken)
    {
        try
        {
            var addressInsert = await AddressFactory.CreateAsync(addressCad.Street,
                addressCad.District, addressCad.ZipCode,
                addressCad.Number, addressCad.AdditionalInformation, 
                addressCad.CityId);

            var insertedAddress = await _addressRepository.AddAddressAsync(addressInsert, cancellationToken);
            var result = await _uow.CommitAsync(cancellationToken);

            return await GetAddressAsync(insertedAddress.Id, cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<AddressResponse> GetAddressAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            var address = await _addressRepository.GetAddressAsync(id, cancellationToken);

            return new AddressResponse
            {
                AdditionalInformation = address.AdditionalInformation,
                City = address.City.Name,
                District = address.District,
                Number = address.Number,
                Street = address.Street,
                ZipCode = address.ZipCode
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<AddressResponse> UpdateAddressAsync(AddressUpdateDTO addressUpdate, CancellationToken cancellationToken)
    {
        try
        {
            var address = await _addressRepository.GetAddressAsync(addressUpdate.Id, cancellationToken, true);

            await address.UpdateAsync(addressUpdate.District, 
                addressUpdate.Street, addressUpdate.AdditionalInformation, 
                addressUpdate.Number, addressUpdate.ZipCode, cancellationToken);

            var updatedAddress = await _addressRepository.UpdateAddressAsync(address, cancellationToken);
            var result = await _uow.CommitAsync(cancellationToken);

            return await GetAddressAsync(addressUpdate.Id, cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }
    }

}