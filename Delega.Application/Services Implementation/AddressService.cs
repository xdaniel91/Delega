using Delega.Application.Exceptions;
using Delega.Application.Repositories_Interfaces;
using Delega.Dominio.Entities;
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
    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<AddressResponse> AddAddressAsync(AddressCreateDTO addressCad, CancellationToken cancellationToken)
    {
        try
        {
            var addressInsert = new Address(addressCad.Street, addressCad.District, addressCad.ZipCode, addressCad.Number, addressCad.AdditionalInformation, addressCad.CityId);

            var valitionResult = await _addressvalidator.ValidateAsync(addressInsert, cancellationToken);

            await ValidarAsync(addressInsert, cancellationToken);

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
            var address = await _addressRepository.GetAddressAsync(addressUpdate.Id, cancellationToken);

            if (addressUpdate.District != null)
                address.District = addressUpdate.District;

            if (addressUpdate.Street != null)
                address.Street = addressUpdate.Street;

            if (addressUpdate.AdditionalInformation != null)
                address.AdditionalInformation = addressUpdate.AdditionalInformation;

            if (addressUpdate.Number != null)
                address.Number = addressUpdate.Number;

            if (addressUpdate.ZipCode != null)
                address.ZipCode = addressUpdate.ZipCode;

            await ValidarAsync(address, cancellationToken);

            var updatedAddress = await _addressRepository.UpdateAddressAsync(address, cancellationToken);
            var result = await _uow.CommitAsync(cancellationToken);

            return new AddressResponse
            {
                AdditionalInformation = updatedAddress.AdditionalInformation,
                City = updatedAddress.City.Name,
                District = updatedAddress.District,
                Number = updatedAddress.Number,
                Street = updatedAddress.Street,
                ZipCode = updatedAddress.ZipCode
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task ValidarAsync(Address address, CancellationToken cancellationToken)
    {
        var valitionResult = await _addressvalidator.ValidateAsync(address, cancellationToken);

        if (!valitionResult.IsValid)
        {
            var errors = valitionResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
            var errorsString = string.Join(",", errors);
            throw new DelegaApplicationException(errorsString);
        }
    }
}
