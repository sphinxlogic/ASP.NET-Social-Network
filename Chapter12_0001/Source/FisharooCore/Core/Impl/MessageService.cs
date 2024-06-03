using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.DataAccess.Impl;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class MessageService : IMessageService
    {
        private IUserSession _userSession;
        private IMessageRepository _messageRepository;
        private IMessageRecipientRepository _messageRecipientRepository; 
        private IAccountRepository _accountRepository;
        private IEmail _email;
        public MessageService()
        {
            _userSession = ObjectFactory.GetInstance<IUserSession>();
            _messageRepository = ObjectFactory.GetInstance<IMessageRepository>();
            _messageRecipientRepository = ObjectFactory.GetInstance<IMessageRecipientRepository>();
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();
            _email = ObjectFactory.GetInstance<IEmail>();
        }

        public void SendMessage(string Body, string Subject, string[] To)
        {
            Message m = new Message();
            m.Body = Body;
            m.Subject = Subject;
            m.CreateDate = DateTime.Now;
            m.MessageTypeID = (int)MessageTypes.Message;
            m.SentByAccountID = _userSession.CurrentUser.AccountID;
            Int64 messageID = _messageRepository.SaveMessage(m);

            //create a copy in the sent items folder for this user
            MessageRecipient sendermr = new MessageRecipient();
            sendermr.AccountID = _userSession.CurrentUser.AccountID;
            sendermr.MessageFolderID = (int) MessageFolders.Sent;
            sendermr.MessageRecipientTypeID = (int) MessageRecipientTypes.TO;
            sendermr.MessageID = messageID;
            sendermr.MessageStatusTypeID = (int)MessageStatusTypes.Unread;
            _messageRecipientRepository.SaveMessageRecipient(sendermr);

            //send to people in the To field
            foreach (string s in To)
            {
                Account toAccount = null;
                if (s.Contains("@"))
                    toAccount = _accountRepository.GetAccountByEmail(s);
                else
                    toAccount = _accountRepository.GetAccountByUsername(s);

                if(toAccount != null)
                {
                    MessageRecipient mr = new MessageRecipient();
                    mr.AccountID = toAccount.AccountID;
                    mr.MessageFolderID = (int)MessageFolders.Inbox;
                    mr.MessageID = messageID;
                    mr.MessageRecipientTypeID = (int) MessageRecipientTypes.TO;
                    _messageRecipientRepository.SaveMessageRecipient(mr);

                    _email.SendNewMessageNotification(_userSession.CurrentUser,toAccount.Email);
                }               
            }
        }
    }
}
