using Events.Core.Shared.Extentions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Events.Core.Shared.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        private PhoneNumber() { }
        public PhoneNumber(string number)
        {
            number.ThrowIfNullOrWhiteSpace(nameof(number));
            if (!Regex.IsMatch(number, "^[0-9]+$"))
            {
                throw new ArgumentException($"Phone number provided is not in the correct format and was: {number}");
            }
            Number = number;
        }
        [MaxLength(255)]
        [Required]
        public string Number { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
