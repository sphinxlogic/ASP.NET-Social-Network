using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;
using System.Linq;
using System.Data.Linq;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class Email : IEmail
    {
        string TO_EMAIL_ADDRESS;
        string FROM_EMAIL_ADDRESS;

        private IConfiguration _configuration;
        private IFriendInvitationRepository _friendInvitationRepository;
        private IAccountRepository _accountRepository;
        private IAlertService _alertService;
        private IUserSession _userSession;
        private IEmailService _emailService;

        public Email()
        {
            _configuration = ObjectFactory.GetInstance<IConfiguration>();
            _friendInvitationRepository = ObjectFactory.GetInstance<IFriendInvitationRepository>();
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();
            _alertService = ObjectFactory.GetInstance<IAlertService>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();
            _emailService = ObjectFactory.GetInstance<IEmailService>();

            TO_EMAIL_ADDRESS = _configuration.ToEmailAddress;
            FROM_EMAIL_ADDRESS = _configuration.FromEmailAddress;
        }

        public void SendNewMessageNotification(Account sender, string ToEmail)
        {
            foreach (string s in ToEmail.Split(new char[] {',',';'}))
            {
                string message = sender.FirstName + " " + sender.LastName +
                " has sent you a message on " + _configuration.SiteName + "!  Please log in at " + _configuration.SiteName + 
                " to view the message.<HR>";

                SendEmail(s, "", "", sender.FirstName + " " + sender.LastName +
                    " has sent you a message on " +
                    _configuration.SiteName + "!", message);
            }
        }

        public string SendInvitations(Account sender, string ToEmailArray, string Message)
        {
            string resultMessage = Message;
            foreach (string s in ToEmailArray.Split(new char[]{',',';'}))
            {
                FriendInvitation friendInvitation = new FriendInvitation();
                friendInvitation.AccountID = sender.AccountID;
                friendInvitation.Email = s;
                friendInvitation.GUID = Guid.NewGuid();
                friendInvitation.BecameAccountID = 0;
                _friendInvitationRepository.SaveFriendInvitation(friendInvitation);

                //add alert to existing users alerts
                Account account = _accountRepository.GetAccountByEmail(s);
                if(account != null)
                {
                    _alertService.AddFriendRequestAlert(_userSession.CurrentUser, account, friendInvitation.GUID,
                                                        Message);
                }

                //CHAPTER 6
                //TODO: MESSAGING - if this email is already in our system add a message through messaging system
                //if(email in system)
                //{
                //      add message to messaging system
                //}
                //else
                //{
                //      send email
                SendFriendInvitation(s, sender.FirstName, sender.LastName, friendInvitation.GUID.ToString(), Message);
                //}
                resultMessage += "• " + s + "<BR>";
            }
            return resultMessage;
        }

        //CHAPTER 5
        public List<string> ParseEmailsFromText(string text)
        {
            List<string> emails = new List<string>();
            string strRegex = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Regex re = new Regex(strRegex, RegexOptions.Multiline);
            foreach (Match m in re.Matches(text))
            {
                string email = m.ToString();
                if(!emails.Contains(email))
                    emails.Add(email);
            }
            return emails;
        }

        //CHAPTER 5
        public void SendFriendInvitation(string toEmail, string fromFirstName, string fromLastName, string GUID, string Message)
        {
            Message = fromFirstName + " " + fromLastName +
            " has invited you to join us at " + _configuration.SiteName + "!<HR><a href=\"" + _configuration.RootURL +
            "Friends/ConfirmFriendshipRequest.aspx?InvitationKey=" + GUID + "\">" + _configuration.RootURL +
            "Friends/ConfirmFriendshipRequest.aspx?InvitationKey=" + GUID + "</a><HR>" + Message;

            SendEmail(toEmail, "", "", fromFirstName + " " + fromLastName + 
                " has invited you to join us at " + 
                _configuration.SiteName + "!", Message);
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
                            Username.Encrypt("verify") + "\">" +
                            _configuration.RootURL + "Accounts/VerifyEmail.aspx?a=" +
                            Username.Encrypt("verify") + "</a>";

            SendEmail(To, "", "", "Account created! Email verification required.", msg);
        }

        public void SendEmail(string From, string Subject, string Message)
        {
            MailMessage mm = new MailMessage(From,TO_EMAIL_ADDRESS);
            mm.Subject = Subject;
            mm.Body = Message;

            _emailService.Send(mm);
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

            _emailService.Send(mm);
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

            _emailService.Send(mm);
        }

        public void SendIndividualEmailsPerRecipient(string[] To, string Subject, string Message)
        {
            foreach (string to in To)
            {
                MailMessage mm = new MailMessage(FROM_EMAIL_ADDRESS,to);
                mm.Subject = Subject;
                mm.Body = Message;
                mm.IsBodyHtml = true;

                _emailService.Send(mm);
            }
        }
    }
}
