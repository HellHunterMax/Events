using Events.Core.Shared.Extentions;
using Events.Core.Shared.ValueObjects;
using System.Text;

namespace Events.Core.Emails.Template
{
    public class EmailTemplate
    {
        private readonly IEmailHeader _header;
        private readonly IEmailBody _body;
        public Email EmailRecipient { get; init; }
        private readonly string _subject;

        public EmailTemplate(Email email, IEmailHeader header, IEmailBody body, string subject)
        {
            EmailRecipient = email.ThrowIfNull(nameof(email));
            _header = header.ThrowIfNull(nameof(header));
            _body = body.ThrowIfNull(nameof(body));
            _subject = subject.ThrowIfNullOrWhiteSpace(nameof(subject));
        }

        public string Content()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_header.Text);
            sb.Append(_body.Text);

            return sb.ToString();
        }

        public string Subject() => _subject;
    }
}
