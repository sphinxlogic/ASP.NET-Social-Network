using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface ITermRepository
    {
        Term GetCurrentTerm();
        void SaveTerm(Term term);
        void DeleteTerm(Term term);
    }
}