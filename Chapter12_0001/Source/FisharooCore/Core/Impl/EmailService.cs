using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class EmailService : IEmailService
    {
        private IConfiguration _configuration;

        public EmailService()
        {
            _configuration = ObjectFactory.GetInstance<IConfiguration>();
        }

        public void Send(MailMessage Message)
        {
            Message.Subject = _configuration.SiteName + " - " + Message.Subject;
            SmtpClient smtp = new SmtpClient();
            smtp.Send(Message);
        }

        public void ProcessEmails()
        {
            throw (new Exception("ProcessEmails is not implemented by this class!"));
        }
    }
}
