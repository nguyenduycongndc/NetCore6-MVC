using ProjectTest.Model;
using ProjectTest.Repo;
using ProjectTest.Repo.Interface;
using ProjectTest.Services.Interface;
using System;
using System.Net;
using System.Net.Mail;

namespace ProjectTest.Services
{
    public class SendMailService : ISendMailService
    {
        private readonly ILogger<SendMailService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserRepo _userRepo;

        public SendMailService(IConfiguration configuration, ILogger<SendMailService> logger, IUserRepo userRepo)
        {
            _logger = logger;
            _configuration = configuration;
            _userRepo = userRepo;
        }
        public Task<bool> SendMailAsync(EmailDto emailDto)
        {
            try
            {
                string[] words = emailDto.To.Split(", ");
                var smtpClient = new SmtpClient(_configuration.GetSection("EmailHost").Value)
                {
                    Port = 587,
                    Credentials = new NetworkCredential(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value),
                    EnableSsl = true,
                };
                if (words.Length > 1)
                {
                    for (int i = 0; i < words.Length; i++)
                    {
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.Subject = emailDto.Subject;
                        mailMessage.Body = emailDto.Body;
                        mailMessage.From = new MailAddress(_configuration.GetSection("EmailUsername").Value);
                        mailMessage.To.Add(new MailAddress(words[i]));
                        mailMessage.IsBodyHtml = true;
                        smtpClient.Send(mailMessage);
                    }
                }
                else
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.Subject = emailDto.Subject;
                    mailMessage.Body = emailDto.Body;
                    mailMessage.From = new MailAddress(_configuration.GetSection("EmailUsername").Value);
                    mailMessage.To.Add(new MailAddress(emailDto.To));
                    mailMessage.IsBodyHtml = true;
                    //var smtpClient = new SmtpClient(_configuration.GetSection("EmailHost").Value)
                    //{
                    //    Port = 587,
                    //    Credentials = new NetworkCredential(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value),
                    //    EnableSsl = true,
                    //};
                    smtpClient.Send(mailMessage);
                }
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
                throw;
            }
        }
        public bool SendMailOTPAsync(string email)
        {
            try
            {
                string OTP = "";
                Random rand = new Random();
                OTP = rand.Next(0, 1000000).ToString("D6");
                var datenow = DateTime.Now;
                var expdate = datenow.AddMinutes(2);
                var rs = _userRepo.CheckEmail(email);
                if (rs.Count == 0)
                {
                    return false;
                }
                else
                {
                    var userUpdateOTP = new UserUpdateOTPModel()
                    {
                        Email = email,
                        OTP = OTP,
                        Expdate = expdate,
                    };
                    _userRepo.UpdateOTPUs(userUpdateOTP);
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.Subject = "OTP thay đổi mật khẩu";
                    mailMessage.Body = "Mã OTP của bạn là: " + OTP;
                    mailMessage.From = new MailAddress(_configuration.GetSection("EmailUsername").Value);
                    mailMessage.To.Add(new MailAddress(email));
                    mailMessage.IsBodyHtml = true;
                    var smtpClient = new SmtpClient(_configuration.GetSection("EmailHost").Value)
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value),
                        EnableSsl = true,
                    };
                    smtpClient.Send(mailMessage);
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
