namespace Fisharoo.FisharooCore.Core
{
    public interface IWebContext
    {
        void ClearSession();
        bool ContainsInSession(string key);
        void RemoveFromSession(string key);
    }
}