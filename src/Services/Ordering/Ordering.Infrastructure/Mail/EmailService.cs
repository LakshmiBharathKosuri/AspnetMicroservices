using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        //EmailSettings is from 'using Ordering.Application.Models'
        public EmailSettings _emailSettings { get; }

        //ILogger is from 'using Microsoft.Extensions.Logging'
        public ILogger<EmailService> _logger { get; }

        //Email settings needs to come from the application settings [appsetting.json]
        // in order to do that we need to use IOptions for EmailSettings.

        //IOptions is from using Microsoft.Extensions.Options
        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            // to read the email settings we have to specify .Value-> 'emailSettings.Value'
            _emailSettings = emailSettings.Value ?? throw new ArgumentNullException(nameof(emailSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //Ref : https://docs.sendgrid.com/for-developers/sending-email/v3-csharp-code-example
        //      https://docs.sendgrid.com/for-developers/sending-email
        public async Task<bool> SendEmail(Email email)
        {
            //to send the email, we are using the sendGrid Client. which expects a ApiKey.

            // creating a client.
            // SendGridClient is from the 'using SendGrid' [from NuGet package]
            // as of now we did not provided any api key, we will see it later.
            var client = new SendGridClient(_emailSettings.ApiKey);
            var subject = email.Subject;

            //EmailAddress is from using SendGrid.Helpers.Mail
            var to = new EmailAddress(email.To);

            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };
            //MailHelper.CreateSingleEmail & SendEmailAsyncis from send grid 
            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);

            _logger.LogInformation("Email sent.");

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            _logger.LogError("Email sending failed.");
            return false;

        }
    }
}
