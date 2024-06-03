using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface IEmail
    {
        void SendEmail(string From, string Subject, string Message);
        void SendEmail(string To, string CC, string BCC, string Subject, string Message);
        void SendEmail(string[] To, string[] CC, string[] BCC, string Subject, string Message);
        void SendIndividualEmailsPerRecipient(string[] To, string Subject, string Message);
        void SendEmailAddressVerificationEmail(string Username, string To);
        void SendPasswordReminderEmail(string To, string EncryptedPassword, string Username);
        
        //CHAPTER 5
        void SendFriendInvitation(string toEmail, string fromFirstName, string fromLastName, string GUID, string Message);
        List<string> ParseEmailsFromText(string text);
        string SendInvitations(Account sender, string ToEmailArray, string Message);
    }
}