using System.Windows.Media.Media3D;

namespace MapEditTool.Rendering3D
{
    public interface IObjectOutlineRenderer
    {
        Model3D Render();
    }
}