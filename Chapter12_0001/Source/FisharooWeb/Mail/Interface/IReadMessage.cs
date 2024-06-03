using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.Mail.Interface
{
    public interface IReadMessage
    {
        void LoadMessage(MessageWithRecipient message);
    }
}
