using Events.Core.Shared.Extentions;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Events.Core.Entities
{
    public class User : IdentityUser
    {
        private User() { }
        public User(string firstName, string lastName)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
        }
        [MaxLength(255)]
        public string FirstName { get; private set; }
        [MaxLength(255)]
        public string LastName { get; private set; }

        public void SetFirstName(string firstName)
        {
            firstName.ThrowIfNullOrWhiteSpace(nameof(firstName));
            if (firstName.Length > 255) throw new ArgumentException($"{nameof(firstName)} Cannot be more than 255 chars but was {firstName.Length}");
            FirstName = firstName;
        }
        public void SetLastName(string lastName)
        {
            lastName.ThrowIfNullOrWhiteSpace(nameof(LastName));
            if (lastName.Length > 255) throw new ArgumentException($"{nameof(lastName)} Cannot be more than 255 chars but was {lastName.Length}");
            LastName = lastName;
        }
        public void SetEmail(string email)
        {
            Email = email.ThrowIfNullOrWhiteSpace(nameof(LastName));
        }
        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber.ThrowIfNullOrWhiteSpace(nameof(PhoneNumber));
        }
    }
}
