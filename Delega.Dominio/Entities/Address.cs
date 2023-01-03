using Delega.Dominio.Exceptions;
using Delega.Dominio.Validators;

namespace Delega.Dominio.Entities;

public class Address : EntityBase
{
    public string Street { get; private set; }
    public string District { get; private set; }
    public string ZipCode { get; private set; }
    public int? Number { get; private set; }
    public string? AdditionalInformation { get; private set; }
    public City City { get; private set; }
    public long CityId { get; private set; }

    public Address()
    {

    }

    public Address(string street, string district, string zipCode, int? number, string? additionalInformation, long cityId)
    {
        Street = street;
        District = district;
        ZipCode = zipCode;
        Number = number;
        AdditionalInformation = additionalInformation;
        CityId = cityId;
        CreatedAt = DateTime.UtcNow;
    }

    private async Task<bool> ValidateAsync(CancellationToken cancellationToken)
    {
        var _addressvalidator = new AddressValidator();

        var valitionResult = await _addressvalidator.ValidateAsync(this, cancellationToken);

        if (!valitionResult.IsValid)
        {
            var errors = valitionResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
            var errorsString = string.Join(",", errors);
            throw new DelegaDomainException(errorsString);
        }

        return true;
    }
    public async Task UpdateAsync(string? district, string? street, string? additionalInfos, int? number, string? zipCode, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            throw new OperationCanceledException("Operação cancelada");

        try
        {
            if (district != null)
                District = district;

            if (street != null)
                Street = street;

            if (additionalInfos != null)
                AdditionalInformation = additionalInfos;

            if (number != null)
                Number = number;

            if (zipCode != null)
                ZipCode = zipCode;

            await ValidateAsync(cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
