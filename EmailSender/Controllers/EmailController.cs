using EmailSender.Application.CQRS.Email;
using EmailSender.Application.DTOs;
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
            var receivers = new List<ReceiverDTO>();

            foreach (var item in sendEmailBindingModel.Receivers)
            {
                var receiver = new ReceiverDTO
                {
                    EmailAddress = item.EmailAddress,
                    Name = item.Name,
                };
                receivers.Add(receiver);
            }

            var result = await _mediator.Send(new SendEmailCommand
            {
                Receivers = receivers,
                EmailTemplate = Encoding.UTF8.GetString(Convert.FromBase64String(sendEmailBindingModel?.EmailTemplate)),
                Subject = sendEmailBindingModel.Subject,
            });

            return Ok();
        }
    }
}
