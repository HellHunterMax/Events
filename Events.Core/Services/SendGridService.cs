using Events.Core.Emails.Template;
using Events.Core.Interfaces;
using Events.Core.Options;
using Events.Core.Shared.Exceptions;
using Events.Core.Shared.OperationResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Events.Core.Services
{
    public class SendGridService : IEmailSenderService
    {
        private readonly SendGridSettings _sendGridSettings;
        private readonly ILogger<SendGridService> _logger;

        public SendGridService(IOptions<SendGridSettings> sendGridSettings, ILogger<SendGridService> logger)
        {
            _sendGridSettings = sendGridSettings.Value;
            _logger = logger;
        }

        public async Task<OperationResult> SendEmailAsync(EmailTemplate email)
        {
            try
            {
                var apiKey = _sendGridSettings.ApiKey;
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(_sendGridSettings.From, _sendGridSettings.Name);
                var to = new EmailAddress(email.EmailRecipient.Address);
                var msg = MailHelper.CreateSingleEmail(from, to, email.Subject(), email.Content(), "");
                var response = await client.SendEmailAsync(msg);

                if (!response.IsSuccessStatusCode) throw new FailedSendMailException(response.StatusCode.ToString());

                return OperationResult.Succeeded();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message} {NewLine} {Stacktrace}", ex.Message, Environment.NewLine, ex.StackTrace);
                return OperationResult.Failed(ex, ex.Message);
            }


        }
    }
}
