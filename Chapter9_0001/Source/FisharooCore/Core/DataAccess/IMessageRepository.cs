using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IMessageRepository
    {
        List<MessageWithRecipient> GetMessagesByAccountID(Int32 AccountID, Int32 PageNumber, MessageFolders Folder);
        MessageWithRecipient GetMessageByMessageID(Int32 MessageID, Int32 RecipientAccountID);
        Int64 SaveMessage(Message message);
        int GetPageCount(MessageFolders messageFolder, Int32 RecipientAccountID);
        void DeleteMessage(Message message);
    }
}