using Business.Abstract;
using DataAccess.Abstract;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using System;
using Core.Entities;
using Core.Entities.Concrete;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        
        IUserService _userService;

        public EmailController(IUserService userService)
        {
            _userService = userService;


        }

        [HttpPost]
        public IActionResult SendMail(string body)
        {

            

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("hassan_burrak@hotmail.com"));


            var pageIndex = 0;
            var pageSize = 20;
            var returnCount = 20;
            var userList = new List<User>();

            while (pageSize == returnCount)
            {
                var result = _userService.Search(0,true,true, string.Empty, string.Empty, string.Empty, pageIndex, pageSize);
                userList.AddRange(result.Contents);
                pageIndex++;
                returnCount = result.Contents.Count;
            }            
            
            foreach (var item in userList)
            {
                email.To.Add(MailboxAddress.Parse(item.EMail)); //Buraya IWantToMail True olanlar gelecek.

            }
                       

                 
            email.Subject = "Test email";
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("hassan_burrak@hotmail.com", "burak1907");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();

        }
    }
}
