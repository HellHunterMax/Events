using Events.Core.Entities;

namespace Events.Core.Emails.Template.Bodies
{
    public class EventCreatedBody : IEmailBody
    {
        private readonly string _text;
        public EventCreatedBody(Event @event)
        {
            _text = $"There is a new Event created to which you are invited to named: {@event.Name}.{Environment.NewLine}{Environment.NewLine}" +
                    $"To Remove yourself from this event follow this link: ";
        }
        public string Text => throw new NotImplementedException();
    }
}
