using Events.Core.Shared.Extentions;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Events.Core.Shared.ValueObjects
{
    public class Email : ValueObject
    {
        private Email() { }
        public Email(string address)
        {
            address.ThrowIfNullOrWhiteSpace(nameof(address));
            if (!Regex.IsMatch(address, @"^(.+)@(.+)$"))
            {
                throw new ArgumentException($"The given email is not a valid email. E-mail: {address}");
            }
            Address = address;
        }
        [MaxLength(320)]
        public string Address { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Address;
        }
        public override string ToString()
        {
            return Address;
        }
    }
}
