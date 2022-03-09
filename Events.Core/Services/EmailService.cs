using Events.Core.Entities;
using Events.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Events.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSenderService _emailSenderService;
        private readonly UserManager<User> _userManager;
        private readonly IEmailFactory _emailFactory;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IEmailSenderService emailSenderService, IEmailFactory emailFactory, ILogger<EmailService> logger, UserManager<User> userManager)
        {
            _emailSenderService = emailSenderService;
            _emailFactory = emailFactory;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task SendEventCreatedMailAsync(Event @event)
        {
            foreach (var address in @event.Attendees)
            {
                var user = await _userManager.FindByEmailAsync(address.Address);
                var email = user != null ? _emailFactory.BuildEventCreatedEmail(@event, user) : _emailFactory.BuildEventCreatedEmail(@event, address);

                var result = await _emailSenderService.SendEmailAsync(email);

                if (!result.Success) _logger.LogError(result.Exception, "{Message} {NewLine} {Stacktrace}", result.Exception!.Message, Environment.NewLine, result.Exception.StackTrace);
            }
        }
    }
}
