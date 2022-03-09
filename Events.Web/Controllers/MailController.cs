using Events.Core.Emails.Template;
using Events.Core.Interfaces;
using Events.Core.Shared.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Events.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IEmailSenderService _mailService;
        private readonly IEmailFactory _emailFactory;

        public MailController(IEmailSenderService mailService, IEmailFactory emailFactory)
        {
            _mailService = mailService;
            _emailFactory = emailFactory;
        }

        [HttpPost("Send E-Mail")]
        public async Task<ActionResult> Post(Email email, string subject, string text, string? html)
        {
            EmailTemplate mailTemplate = _emailFactory.BuildGenericEmail(email, subject, text);
            var result = await _mailService.SendEmailAsync(mailTemplate);

            if (!result.Success) return BadRequest(result.Message);

            return NoContent();
        }
    }
}
