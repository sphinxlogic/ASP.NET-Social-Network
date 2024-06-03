using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class MessageFolderRepository : IMessageFolderRepository
    {
        private Connection conn;
        public MessageFolderRepository()
        {
            conn = new Connection();
        }

        public MessageFolder GetMessageFolderByID(Int32 MessageFolderID)
        {
            MessageFolder result = null;

            using (FisharooDataContext dc = conn.GetContext())
            {
                result = dc.MessageFolders.Where(mf => mf.MessageFolderID == MessageFolderID).FirstOrDefault();
            }

            return result;
        }

        public List<MessageFolder> GetMessageFoldersByAccountID(Int32 AccountID)
        {
            List<MessageFolder> result = new List<MessageFolder>();
            using (FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<MessageFolder> systemFolders = dc.MessageFolders.Where(mf => mf.IsSystem == true);
                IEnumerable<MessageFolder> userFolders = dc.MessageFolders.Where(mf => mf.AccountID == AccountID);
                result = systemFolders.Union(userFolders).ToList();
            }
            return result;
        }

        public void SaveMessageFolder(MessageFolder messageFolder)
        {
            using (FisharooDataContext dc = conn.GetContext())
            {
                if (messageFolder.MessageFolderID > 0)
                {
                    dc.MessageFolders.Attach(messageFolder, true);
                }
                else
                {
                    dc.MessageFolders.InsertOnSubmit(messageFolder);
                }
                dc.SubmitChanges();
            }
        }

        public void DeleteMessageFolder(MessageFolder messageFolder)
        {
            using (FisharooDataContext dc = conn.GetContext())
            {
                dc.MessageFolders.Attach(messageFolder, true);
                dc.MessageFolders.DeleteOnSubmit(messageFolder);
                dc.SubmitChanges();
            }
        }
    }
}
