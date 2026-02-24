using System.Windows.Media;
using System.Windows.Media.Media3D;
using MapEditTool.Models.Map;

namespace MapEditTool.Rendering3D
{
    public class CubeRenderer : IBlockRenderer
    {
        public Model3D Render(BlockData block, bool isGhost = false)
        {
            var mesh = new MeshGeometry3D();

            mesh.Positions = new Point3DCollection
            {
                new(0,0,0),
                new(1,0,0),
                new(1,1,0),
                new(0,1,0),

                new(0,0,1),
                new(1,0,1),
                new(1,1,1),
                new(0,1,1)
            };

            mesh.TriangleIndices = new Int32Collection
            {
                // 前面
                0,2,1, 0,3,2,
                // 背面
                4,5,6, 4,6,7,
                // 左
                0,7,3, 0,4,7,
                // 右
                1,2,6, 1,6,5,
                // 下
                0,1,5, 0,5,4,
                // 上
                3,7,6, 3,6,2
            };

            var material = BlockMaterialFactory.Create(block.Type, isGhost);

            return new GeometryModel3D(mesh, material);
        }
    }
}
