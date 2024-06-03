using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Fisharoo.FisharooCore.Core.Impl;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    public class MailMessageService
    {
        
        public MailMessageService()
        {
            
        }

        public static string SerializeEncrypted(MailMessage MailMessage)
        {
            string result = Serialize(MailMessage);
            result = Cryptography.Encrypt(result, "SomeSaltAndPepper");
            return result;
        }

        public static MailMessage DeserializeEncrypted(string SerializedAndEncryptedMyMailMessage)
        {
            string result = Cryptography.Decrypt(SerializedAndEncryptedMyMailMessage, "SomeSaltAndPepper");
            MailMessage mm = Deserialize(result);
            return mm;
        }

        public static string Serialize(MailMessage MailMessage)
        {
            string result = "";
            MyMailMessage mmm = new MyMailMessage();
            mmm.Bcc = ConvertMailAddressToMyMailAddress(MailMessage.Bcc);
            mmm.Body = MailMessage.Body;
            mmm.Cc = ConvertMailAddressToMyMailAddress(MailMessage.CC);
            mmm.From = ConvertMailAddressToMyMailAddress(MailMessage.From);
            mmm.IsBodyHtml = MailMessage.IsBodyHtml;
            mmm.ReplyTo = ConvertMailAddressToMyMailAddress(MailMessage.ReplyTo);
            mmm.Sender = ConvertMailAddressToMyMailAddress(MailMessage.Sender);
            mmm.Subject = MailMessage.Subject;
            mmm.To = ConvertMailAddressToMyMailAddress(MailMessage.To);

            result = XMLService.Serialize(mmm);
            return result;
        }

        public static MailMessage Deserialize(string SerializedMyMailMessage)
        {
            MyMailMessage mmm = XMLService.Deserialize<MyMailMessage>(SerializedMyMailMessage);
            MailMessage mm = new MailMessage();

            foreach (var a in mmm.To)
            {
                mm.To.Add(ConvertMyMailAddressToMailAddress(a));
            }

            foreach (var a in mmm.Cc)
            {
                mm.CC.Add(ConvertMyMailAddressToMailAddress(a));
            }

            foreach (var a in mmm.Bcc)
            {
                mm.Bcc.Add(ConvertMyMailAddressToMailAddress(a));
            }

            mm.Body = mmm.Body;
            mm.IsBodyHtml = mmm.IsBodyHtml;
            mm.ReplyTo = ConvertMyMailAddressToMailAddress(mmm.ReplyTo);
            mm.Sender = ConvertMyMailAddressToMailAddress(mmm.Sender);
            mm.Subject = mmm.Subject;
            mm.From = ConvertMyMailAddressToMailAddress(mmm.From);

            return mm;
        }

        private static MailAddressCollection ConvertMyMailAddressesToMailAddresses(List<MyMailMessage.MailAddress> MyMailAddresses)
        {
            MailAddressCollection mac = new MailAddressCollection();
            foreach(var a in MyMailAddresses)
            {
                mac.Add(ConvertMyMailAddressToMailAddress(a));
            }
            return mac;
        }

        private static MailAddress ConvertMyMailAddressToMailAddress(MyMailMessage.MailAddress MyMailAddress)
        {
            MailAddress ma = null;
            if(MyMailAddress != null && MyMailAddress.Address != null && MyMailAddress.DisplayName != null)
                ma = new MailAddress(MyMailAddress.Address, MyMailAddress.DisplayName);
            return ma;
        }

        private static MyMailMessage.MailAddress[] ConvertMailAddressToMyMailAddress(MailAddressCollection MailAddresses)
        {
            List<MyMailMessage.MailAddress> result = new List<MyMailMessage.MailAddress>();
            foreach (var a in MailAddresses)
            {
                result.Add(ConvertMailAddressToMyMailAddress(a));
            }
            return result.ToArray();
        }

        private static MyMailMessage.MailAddress ConvertMailAddressToMyMailAddress(MailAddress MailAddress)
        {
            MyMailMessage.MailAddress ma = new MyMailMessage.MailAddress();
            if (MailAddress != null)
            {
                ma.Address = MailAddress.Address;
                ma.DisplayName = MailAddress.DisplayName;
            }

            return ma;
        }
    }

    [Serializable]
    public class MyMailMessage
    {
        public MailAddress[] Bcc { get; set; }
        public MailAddress[] Cc { get; set; }
        public MailAddress[] To { get; set; }
        public string Body { get; set; }
        public MailAddress From { get; set; }
        public bool IsBodyHtml { get; set; }
        public MailAddress ReplyTo { get; set; }
        public MailAddress Sender { get; set; }
        public string Subject { get; set; }

        [Serializable]
        public class MailAddress
        {
            public string Address { get; set; }
            public string DisplayName { get; set; }
        } 
    }
}