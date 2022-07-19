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
using Entities.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        
        IUserService _userService;
        IEmailService _emailService;

        public EmailController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;

        }

        [HttpPost]
        public IActionResult SendMail(EmailDto request)
        {
            _emailService.SendEmail(request);
            return Ok();

        }
    }
}
