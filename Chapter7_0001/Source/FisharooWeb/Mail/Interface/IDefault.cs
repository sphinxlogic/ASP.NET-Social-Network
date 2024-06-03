using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.Mail.Interface
{
    public interface IDefault
    {
        void LoadMessages(List<MessageWithRecipient> Messages);
        List<Int32> ExtractSelectedMessages();
        void DisplayPageNavigation(Int32 PageCount, MessageFolders folder, Int32 CurrentPage);
    }
}
