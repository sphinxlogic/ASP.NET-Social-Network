using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.DataAccess.Impl;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("MailQueue")]
    public class DBMailService : IEmailService
    {
        private IEmailRepository _emailRepository;
        private IConfiguration _configuration;
        private Connection _conn;
        public DBMailService()
        {
            _conn = new Connection();
            _configuration = ObjectFactory.GetInstance<IConfiguration>();
            _emailRepository = ObjectFactory.GetInstance<IEmailRepository>();
        }

        public void Send(MailMessage Message)
        {
            Message.Subject = _configuration.SiteName + " - " + Message.Subject;

            MailQueue_Receiving mq = new MailQueue_Receiving();
            mq.CreateDate = DateTime.Now;
            mq.SerializedMailMessage = Message.Serialize();
            mq.SendDate = Convert.ToDateTime("1/1/2000");

            _emailRepository.Save(mq);
        }

        public void ProcessEmails()
        {
            //make sure we are only processing this in one thread! 
            //otherwise we might lose emails
            lock (this)
            {
                try
                {
                    List<pr_MailQueue_SwapReceivingAndWorking_GetWorkingResult> results =
                        new List<pr_MailQueue_SwapReceivingAndWorking_GetWorkingResult>();

                    results = _emailRepository.GetMailQueueToProcess();

                    foreach (var result in results)
                    {
                        MailMessage mm = XMLService.Deserialize<MailMessage>(result.SerializedMailMessage);
                        SmtpClient smtp = new SmtpClient();
                        smtp.Send(mm);
                    }

                    _emailRepository.MoveMailQueueWorkingToHistory();
                }
                catch(Exception e)
                {
                    Log.Fatal(this, e.Message);
                    return;
                }
            }
        }
    }
}
