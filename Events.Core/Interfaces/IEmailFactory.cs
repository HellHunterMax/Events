using Events.Core.Emails.Template;
using Events.Core.Entities;
using Events.Core.Shared.ValueObjects;

namespace Events.Core.Interfaces
{
    public interface IEmailFactory
    {
        public EmailTemplate BuildGenericEmail(Email email, string subject, string text);
        public EmailTemplate BuildEventCreatedEmail(Event @event, Email email);
        public EmailTemplate BuildEventCreatedEmail(Event @event, User user);
    }
}
