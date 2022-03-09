using Events.Core.Entities;

namespace Events.Core.Interfaces
{
    public interface IEmailService
    {
        public Task SendEventCreatedMailAsync(Event @event);
    }
}
