using Business.Abstract;
using Entities.DTOs;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Configuration;

namespace Business.Concrete
{
    public class EmailManager : IEmailService
    {
        //public IConfiguration _config { get; }
        
        //public EmailManager(IConfiguration config)
        //{
        //    _config = config;

        //}
        //public void SendEmail(EmailDto request)
        //{
        //    var email = new MimeMessage();
        //    email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
        //    email.To.Add(MailboxAddress.Parse(request.To)); //Buraya IWantToMail True olanlar gelecek.
        //    email.Subject =request.Subject;
        //    email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

        //    using var smtp = new SmtpClient();
        //    smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
        //    smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
        //    smtp.Send(email);
        //    smtp.Disconnect(true);
        //}
    }
}
