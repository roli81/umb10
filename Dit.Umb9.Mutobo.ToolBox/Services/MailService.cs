using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.Config;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Web;

namespace Dit.Umb9.Mutobo.ToolBox.Services
{
    public class MailService : BaseService, IMailService
    {

        private readonly IConfigService _configurationService;

        public MailService(
            ILogger<MailService> logger, 
            IUmbracoContextAccessor contextAccessor, 
            IConfigService configurationService) : base(logger, contextAccessor)
        {
            _configurationService = configurationService;
        }

        public void SendConfirmationMail(ContactFormData model)
        {
            var custConfig = new MailConfiguration(Context.Content.GetById(model.SenderMailConfigId));

            var strBuilder = new StringBuilder();

            strBuilder.Append("<html>");
            strBuilder.Append("<head>");
            strBuilder.Append("</head>");
            strBuilder.Append("<body>");

            strBuilder.Append(custConfig.MailHeader);
            strBuilder.Append(custConfig.MailBody);
            strBuilder.Append($"<p>Name: {model.Name}<br />");
            strBuilder.Append($"Email: {model.Email}<br />");
            strBuilder.Append($"Nachricht: {model.Comment}<br /></p>");
            strBuilder.Append(custConfig.MailFooter);


            strBuilder.Append("</body>");
            strBuilder.Append("</html>");


            var sendMail = new MailMessage()
            {
                From = new MailAddress(custConfig.SenderMail),
                Subject = custConfig.MailSubject,
                Body = strBuilder.ToString(),
                IsBodyHtml = true
            };

            sendMail.To.Add(model.Email);



            SendMail(sendMail);
        }

        public void SendContactMail(ContactFormData model)
        {
            var recConfig = new MailConfiguration(Context.Content.GetById(model.ReceiverMailConfigId));
            var strBuilder = new StringBuilder();
            strBuilder.Append("<html>");
            strBuilder.Append("<head>");
            strBuilder.Append("</head>");
            strBuilder.Append("<body>");
            strBuilder.Append(recConfig.MailHeader);
            strBuilder.Append(recConfig.MailBody);
            strBuilder.Append($"<p>Name: {model.Name}<br />");
            strBuilder.Append($"Email: {model.Email}<br />");
            strBuilder.Append($"Nachricht: {model.Comment}<br /></p>");
            strBuilder.Append(recConfig.MailFooter);
            strBuilder.Append("</body>");
            strBuilder.Append("</html>");

            var sendMail = new MailMessage()
            {
                From = new MailAddress(recConfig.SenderMail),
                Subject = recConfig.MailSubject,
                Body = strBuilder.ToString(),
                IsBodyHtml = true
            };

            string[] multipleMails = recConfig.ReceiverMail.Split(';');
            foreach (string tmpMails in multipleMails)
            {
                sendMail.To.Add(tmpMails);
            }
            SendMail(sendMail);
        }

        private void SendMail(MailMessage mail)
        {
            var mailServer = _configurationService.GetAppSettingValue("Umbraco:Cms:Global:Smtp:Host");
            var port = _configurationService.GetAppSettingIntValue("Umbraco:Cms:Global:Smtp:Port");
            var userName = _configurationService.GetAppSettingValue("Umbraco:Cms:Global:Smtp:Username");
            var pwd = _configurationService.GetAppSettingValue("Umbraco:Cms:Global:Smtp:Password");
            var smtpClient = port.HasValue ? new SmtpClient(mailServer, port.Value) : new SmtpClient(mailServer);
            smtpClient.Credentials = new NetworkCredential(userName, pwd);
            smtpClient.EnableSsl = true;
            smtpClient.Send(mail);
        }
    }
}
