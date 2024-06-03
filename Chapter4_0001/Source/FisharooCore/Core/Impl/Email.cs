using System.Net.Mail;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class Email : IEmail
    {
        const string TO_EMAIL_ADDRESS = "website@fisharoo.com";
        const string FROM_EMAIL_ADDRESS = "website@fisharoo.com";

        private IConfiguration _configuration;

        public Email()
        {
            _configuration = ObjectFactory.GetInstance<IConfiguration>();
        }

        public void SendPasswordReminderEmail(string To, string EncryptedPassword, string Username)
        {
            string Message = "Here is the password you requested: " +
                             Cryptography.Decrypt(EncryptedPassword, Username);
            SendEmail(To, "", "", "Password Reminder", Message);
        }

        public void SendEmailAddressVerificationEmail(string Username, string To)
        {
            string msg = "Please click on the link below or paste it into a browser to verify your email account.<BR><BR>" +
                            "<a href=\"" + _configuration.RootURL + "Accounts/VerifyEmail.aspx?a=" +
                            Cryptography.Encrypt(Username, "verify") + "\">" +
                            _configuration.RootURL + "Accounts/VerifyEmail.aspx?a=" +
                            Cryptography.Encrypt(Username, "verify") + "</a>";

            SendEmail(To, "", "", "Account created! Email verification required.", msg);
        }

        public void SendEmail(string From, string Subject, string Message)
        {
            MailMessage mm = new MailMessage(From,TO_EMAIL_ADDRESS);
            mm.Subject = Subject;
            mm.Body = Message;
            
            Send(mm);
        }

        public void SendEmail(string To, string CC, string BCC, string Subject, string Message)
        {
            MailMessage mm = new MailMessage(FROM_EMAIL_ADDRESS,To);

            if(!string.IsNullOrEmpty(CC))
                mm.CC.Add(CC);

            if(!string.IsNullOrEmpty(BCC))
                mm.Bcc.Add(BCC);

            mm.Subject = Subject;
            mm.Body = Message;
            mm.IsBodyHtml = true;

            Send(mm);
        }

        public void SendEmail(string[] To, string[] CC, string[] BCC, string Subject, string Message)
        {
            MailMessage mm = new MailMessage();
            foreach (string to in To)
            {
                mm.To.Add(to);   
            }
            foreach (string cc in CC)
            {
                mm.CC.Add(cc);
            }
            foreach (string bcc in BCC)
            {
                mm.Bcc.Add(bcc);
            }
            mm.From = new MailAddress(FROM_EMAIL_ADDRESS);
            mm.Subject = Subject;
            mm.Body = Message;
            mm.IsBodyHtml = true;

            Send(mm);
        }

        public void SendIndividualEmailsPerRecipient(string[] To, string Subject, string Message)
        {
            foreach (string to in To)
            {
                MailMessage mm = new MailMessage(FROM_EMAIL_ADDRESS,to);
                mm.Subject = Subject;
                mm.Body = Message;
                mm.IsBodyHtml = true;

                Send(mm);
            }
        }

        private void Send(MailMessage Message)
        {
            Message.Subject = _configuration.SiteName + " - " + Message.Subject;
            SmtpClient smtp = new SmtpClient();
            smtp.Send(Message);
        }
    }
}
