using aspnetTutorial.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace aspnetTutorial.Services
{
    public class EmailService : IEmailService
    {

        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModal _smpconfig;

        public EmailService(IOptions<SMTPConfigModal> smpconfig)
        {
            _smpconfig = smpconfig.Value;
        }
        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Hello {{UserName}} This is some doomy email from Email", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("Email"),userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);
        }
        public async Task SendConfirmationEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Hola {{UserName}} Confirma tu email", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(File.ReadAllText("wwwroot/EmailTemplate/Email.html"), userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);
        }

        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage()
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smpconfig.SenderAddress, _smpconfig.SenderDisplayName),
                IsBodyHtml = _smpconfig.IsBodyHTML,
            };

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }
            NetworkCredential networkCredential = new NetworkCredential(_smpconfig.UserName, _smpconfig.Password);
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smpconfig.Host,
                Port = _smpconfig.Port,
                EnableSsl = _smpconfig.EnableSSL,
                UseDefaultCredentials = _smpconfig.UseDefaultCredentials,
                Credentials = networkCredential
            };
            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
        }

        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }

        private string UpdatePlaceHolders(string text,List<KeyValuePair<string,string>> keyValuePairs)
        {
            if(!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {

                foreach(var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }

              
            }
            return text;
        }
    }
}
