using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class MessageRecipientRepository : IMessageRecipientRepository
    {
        private Connection conn;
        public MessageRecipientRepository()
        {
            conn = new Connection();
        }

        public List<MessageRecipient> GetMessageRecipientsByMessageID(Int32 MessageID)
        {
            List<MessageRecipient> result = new List<MessageRecipient>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<MessageRecipient> recipients = dc.MessageRecipients.Where(mr => mr.MessageID == MessageID);
                result = recipients.ToList();
            }
            return result;
        }

        public MessageRecipient GetMessageRecipientByID(Int32 MessageRecipientID)
        {
            MessageRecipient result = null;
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.MessageRecipients.Where(mr => mr.MessageRecipientID == MessageRecipientID).FirstOrDefault();
            }
            return result;
        }

        public void SaveMessageRecipient(MessageRecipient messageRecipient)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                if(messageRecipient.MessageRecipientID > 0)
                {
                    dc.MessageRecipients.Attach(messageRecipient, true);
                }
                else
                {
                    dc.MessageRecipients.InsertOnSubmit(messageRecipient);
                }
                dc.SubmitChanges();
            }
        }

        public void DeleteMessageRecipient(MessageRecipient messageRecipient)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.MessageRecipients.Attach(messageRecipient,true);
                dc.MessageRecipients.DeleteOnSubmit(messageRecipient);
                dc.SubmitChanges();

                //if the last recipient was deleted
                //...also delete the message
                int RemainingRecipientCount =
                    dc.MessageRecipients.Where(mr => mr.MessageID == messageRecipient.MessageID).Count();
                if (RemainingRecipientCount == 0)
                {
                    dc.Messages.DeleteOnSubmit(
                        dc.Messages.Where(m => m.MessageID == messageRecipient.MessageID).FirstOrDefault());
                    dc.SubmitChanges();
                }
            }
        }
    }
}
