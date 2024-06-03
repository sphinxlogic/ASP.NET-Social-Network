using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IMessageRecipientRepository
    {
        List<MessageRecipient> GetMessageRecipientsByMessageID(Int32 MessageID);
        MessageRecipient GetMessageRecipientByID(Int32 MessageRecipientID);
        void SaveMessageRecipient(MessageRecipient messageRecipient);
        void DeleteMessageRecipient(MessageRecipient messageRecipient);
    }
}