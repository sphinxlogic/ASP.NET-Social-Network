using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooCore.Core.Impl;
using StructureMap;

namespace FisharooMailQueueProcessor
{
public class Program
{
    static void Main(string[] args)
    {
        //you can use the InjectStub to tell ObjectFactory 
        //to return a different type of class
        //other than the default type
        ObjectFactory.InjectStub(typeof(IEmailService), new DBMailService());

        IEmailService _emailService = ObjectFactory.GetInstance<IEmailService>();
        
        _emailService.ProcessEmails();

        //but make sure you reset it to your defaults
        //when you are done - this could be a source
        //of a bug if you forget!
        ObjectFactory.ResetDefaults();
    }
}
}
