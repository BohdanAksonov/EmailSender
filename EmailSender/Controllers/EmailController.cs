using EmailSender.Application.CQRS.Email;
using EmailSender.BindingModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EmailSender.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IMediator _mediator;

        public EmailController(ILogger<EmailController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("send-email")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailBindingModel sendEmailBindingModel)
        {
            var result = await _mediator.Send(new SendEmailCommand
            {
                Receivers = sendEmailBindingModel.Receivers,
                EmailTemplate = Encoding.UTF8.GetString(Convert.FromBase64String(sendEmailBindingModel?.EmailTemplate)),
                Subject = sendEmailBindingModel.Subject,
                Credential = sendEmailBindingModel.Credential,
                From = sendEmailBindingModel.From,
            });

            return Ok();
        }
    }
}
