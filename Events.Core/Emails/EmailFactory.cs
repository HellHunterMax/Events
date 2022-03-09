using Events.Core.Emails.Template;
using Events.Core.Emails.Template.Bodies;
using Events.Core.Emails.Template.Headers;
using Events.Core.Entities;
using Events.Core.Interfaces;
using Events.Core.Shared.ValueObjects;

namespace Events.Core.Emails
{
    public class EmailFactory : IEmailFactory
    {
        public EmailTemplate BuildEventCreatedEmail(Event @event, User user)
        {
            var address = new Email(user.Email);
            return new EmailTemplate(address, new EmailHeader(user), new EventCreatedBody(@event), $"New Event Created: {@event.Name}");
        }
        public EmailTemplate BuildEventCreatedEmail(Event @event, Email email)
        {
            return new EmailTemplate(email, new EmailHeader(email), new EventCreatedBody(@event), $"New Event Created: {@event.Name}");
        }

        public EmailTemplate BuildGenericEmail(Email email, string subject, string text)
        {
            return new EmailTemplate(email, new EmailHeader(email), new GenericEmailBody(text), subject);
        }
    }
}
