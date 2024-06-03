using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IMessageFolderRepository
    {
        MessageFolder GetMessageFolderByID(Int32 MessageFolderID);
        List<MessageFolder> GetMessageFoldersByAccountID(Int32 AccountID);
        void SaveMessageFolder(MessageFolder messageFolder);
        void DeleteMessageFolder(MessageFolder messageFolder);
    }
}