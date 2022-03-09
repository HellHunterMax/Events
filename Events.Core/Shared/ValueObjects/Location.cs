using Events.Core.Shared.Extentions;
using System.ComponentModel.DataAnnotations;

namespace Events.Core.Shared.ValueObjects
{
    public class Location : ValueObject
    {
        private Location() { }
        public Location(string streetName, string houseNumber, string city, string postalCode, float latitude, float longitude)
        {
            StreetName = streetName.ThrowIfNullOrWhiteSpace(nameof(streetName));
            HouseNumber = houseNumber.ThrowIfNullOrWhiteSpace(nameof(HouseNumber));
            City = city.ThrowIfNullOrWhiteSpace(nameof(city));
            PostalCode = postalCode.ThrowIfNullOrWhiteSpace(nameof(postalCode));
            Latitude = latitude;
            Longitude = longitude;
        }

        [MaxLength(255)]
        public string StreetName { get; private set; }
        [MaxLength(255)]
        public string HouseNumber { get; private set; }
        [MaxLength(255)]
        public string City { get; private set; }
        [MaxLength(255)]
        public string PostalCode { get; private set; }
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StreetName;
            yield return HouseNumber;
            yield return City;
            yield return PostalCode;
            yield return Longitude;
            yield return Latitude;
        }

        public override string ToString()
        {
            return $"{StreetName} {HouseNumber}, {City} {PostalCode},Latitude: {Latitude}, Longitude: {Longitude}";
        }
    }
}
