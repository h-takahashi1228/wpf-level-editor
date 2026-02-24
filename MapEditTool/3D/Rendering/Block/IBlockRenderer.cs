using System.Windows.Media.Media3D;
using MapEditTool.Models.Map;

namespace MapEditTool.Rendering3D
{
    public interface IBlockRenderer
    {
        Model3D Render(BlockData block, bool isGhost = false);
    }
}