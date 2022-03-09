using Events.Core.Entities;
using Events.Core.Shared.ValueObjects;

namespace Events.Core.Emails.Template.Headers
{
    public class EmailHeader : IEmailHeader
    {
        public EmailHeader(Email email)
        {
            Email = email;
        }
        public EmailHeader(User user)
        {
            User = user;
        }
        private readonly User? User;
        private readonly Email? Email;

        public string Text => User is null ? $"Dear {Email},{Environment.NewLine}{Environment.NewLine}" : $"Dear {User.FirstName} {User.LastName},{Environment.NewLine}{Environment.NewLine}";
    }
}
