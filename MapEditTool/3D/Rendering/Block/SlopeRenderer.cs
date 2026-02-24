using System.Windows.Media;
using System.Windows.Media.Media3D;
using MapEditTool.Models.Map;

namespace MapEditTool.Rendering3D
{
    public class SlopeRenderer : IBlockRenderer
    {
        public Model3D Render(BlockData block, bool isGhost = false)
        {
            var mesh = new MeshGeometry3D();

            mesh.Positions = new Point3DCollection
            {
                new(0, 0, 0),
                new(1, 0, 0),
                new(1, 0, 1),
                new(0, 0, 1),

                new(0, 1, 1),
                new(1, 1, 1)
            };

            mesh.TriangleIndices = new Int32Collection
            {
                // 底面
                0,1,2, 0,2,3,

                // 斜面
                3,2,5, 3,5,4,

                // 左側
                0,3,4,

                // 右側
                1,5,2,

                // 背面
                0,4,5, 0,5,1
            };

            var material = BlockMaterialFactory.Create(block.Type, isGhost);

            return new GeometryModel3D(mesh, material);
        }
    }
}
