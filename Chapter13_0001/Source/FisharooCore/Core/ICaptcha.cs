using System.Drawing;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface ICaptcha
    {
        string FamilyName { get; set;  }
        string Text { get; set; }
        Bitmap Image { get; }
        int Width { get; set; }
        int Height { get; set; }
        void Dispose();
    }
}