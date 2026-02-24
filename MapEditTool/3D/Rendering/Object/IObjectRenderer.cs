using MapEditTool.Models.Map.ObjectData;
using System.Windows.Media.Media3D;

namespace MapEditTool.Rendering3D
{
    public interface IObjectRenderer
    {
        Model3D Render(ObjectData obj, bool isGhost = false, bool canPlace = true);
    }
}