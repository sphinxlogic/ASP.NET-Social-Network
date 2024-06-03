using System.Net.Mail;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface IEmailService
    {
        void Send(MailMessage Message);
        void ProcessEmails();
    }
}