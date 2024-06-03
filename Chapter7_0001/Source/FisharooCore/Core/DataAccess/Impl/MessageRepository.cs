using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class MessageRepository : IMessageRepository
    {
        private Connection conn;
        public MessageRepository()
        {
            conn = new Connection();
        }

        public int GetPageCount(MessageFolders messageFolder, Int32 RecipientAccountID)
        {
            int result = 0;
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = ((from r in dc.MessageRecipients
                          where r.AccountID == RecipientAccountID &&
                                r.MessageFolderID == (int) messageFolder
                          select r).Count());
            }
            if (result < 10)
                result = 1;
            else
            {
                if (result % 10 == 0)
                    result = result / 10;
                else
                    result = (result / 10) + 1;
            }
            return result;
        }
        public List<MessageWithRecipient> GetMessagesByAccountID(Int32 AccountID, Int32 PageNumber, MessageFolders Folder)
        {
            List<MessageWithRecipient> result = new List<MessageWithRecipient>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<MessageWithRecipient> messages = (from r in dc.MessageRecipients 
                                                              join m in dc.Messages on r.MessageID equals m.MessageID 
                                                              join a in dc.Accounts on m.SentByAccountID equals a.AccountID
                                                 where r.AccountID == AccountID && r.MessageFolderID == (int)Folder
                                                 orderby m.CreateDate descending
                                                 select new MessageWithRecipient()
                                                            {
                                                                Sender = a,
                                                                Message = m,
                                                                MessageRecipient = r
                                                            }).Skip((PageNumber - 1)*10).Take(10);
                result = messages.ToList();
            }
            return result;
        }

        public MessageWithRecipient GetMessageByMessageID(Int32 MessageID, Int32 RecipientAccountID)
        {
            MessageWithRecipient message = null;
            using(FisharooDataContext dc = conn.GetContext())
            {
                message = (from r in dc.MessageRecipients 
                          join m in dc.Messages on r.MessageID equals m.MessageID 
                          join a in dc.Accounts on m.SentByAccountID equals a.AccountID
                               where r.AccountID == RecipientAccountID &&
                               m.MessageID == MessageID
                           where r.AccountID == RecipientAccountID && m.MessageID == MessageID
                           select new MessageWithRecipient()
                            {
                                Sender = a,
                                Message = m,
                                MessageRecipient = r
                            }).FirstOrDefault();
            }
            return message;
        }

        public Int64 SaveMessage(Message message)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                if(message.MessageID > 0)
                {
                    dc.Messages.Attach(message, true);
                }
                else
                {
                    message.CreateDate = DateTime.Now;
                    dc.Messages.InsertOnSubmit(message);
                }
                dc.SubmitChanges();
            }
            return message.MessageID;
        }

        /// <summary>
        /// DeleteMessage deletes a message and all of its recipients!
        /// </summary>
        /// <param name="message">The message to be deleted</param>
        public void DeleteMessage(Message message)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<MessageRecipient> recipients = dc.MessageRecipients.Where(mr => mr.MessageID == message.MessageID);
                foreach(MessageRecipient mr in recipients)
                {
                    dc.MessageRecipients.DeleteOnSubmit(mr);
                }
                dc.SubmitChanges();
                dc.Messages.DeleteOnSubmit(message);
                dc.SubmitChanges();
            }
        }
    }
}
