using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface IMessageService
    {
        void SendMessage(string Body, string Subject, string[] To);
    }
}