﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fisharoo.FisharooCore.Core.Domain
{
    public class MessageWithRecipient
    {
        public Account Sender { get; set; }
        public Message Message { get; set; }
        public MessageRecipient MessageRecipient{ get; set; }
    }
}
