namespace Delega.Dominio.Entities;

public class Address : EntityBase
{
    public string Street { get; set; }
    public string District { get; set; }
    public string ZipCode { get; set; }
    public int? Number { get; set; }
    public string? AdditionalInformation { get; set; }
    public City City { get; set; }
    public long CityId { get; set; }

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
    }
}
