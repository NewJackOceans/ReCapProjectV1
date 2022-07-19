using Business.Abstract;
using Entities.DTOs;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Configuration;
using Core.Utilities.Results;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class EmailManager : IEmailService
    {
        IUserService _userService;
        private readonly IConfiguration _configuration;

        public EmailManager(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        public void SendEmail(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));


            var pageIndex = 0;
            var pageSize = 20;
            var returnCount = 20;
            var userList = new List<User>();

            while (pageSize == returnCount)
            {
                var result = _userService.Search(0, true, true, string.Empty, string.Empty, string.Empty, pageIndex, pageSize);
                userList.AddRange(result.Contents);
                pageIndex++;
                returnCount = result.Contents.Count;
            }

            foreach (var item in userList)
            {
                email.To.Add(MailboxAddress.Parse(item.EMail)); //Buraya IWantToMail True olanlar gelecek.
            }



            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

            
        }
    }
}
