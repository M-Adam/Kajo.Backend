using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Kajo.Backend.Common
{
    public class MailSender : IMailSender
    {
        private readonly SendGridClient _client;

        public MailSender()
        {
            //ToDo: add key
            _client = new SendGridClient("asdf");
        }

        public async Task ShareChecklist(string checklistName, string sender, string recipient)
        {
            var email = MailHelper.CreateSingleEmail(new EmailAddress(sender), new EmailAddress(recipient), "Checklist shared",
                $"{sender} has shared his {checklistName} checklist with you.", null);
            await _client.SendEmailAsync(email);
        }
    }

    public interface IMailSender
    {
        Task ShareChecklist(string checklistName, string sender, string recipient);
    }
}
