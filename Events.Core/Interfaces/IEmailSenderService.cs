using Events.Core.Emails.Template;
using Events.Core.Shared.OperationResults;

namespace Events.Core.Interfaces
{
    public interface IEmailSenderService
    {
        Task<OperationResult> SendEmailAsync(EmailTemplate email);
    }
}
