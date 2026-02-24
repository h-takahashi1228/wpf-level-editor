using MapEditTool.Models.Map.ObjectData;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MapEditTool.Rendering3D
{
    public class DefenseBaseRenderer : IObjectRenderer
    {
        public Model3D Render(ObjectData obj, bool isGhost = false, bool canPlace = true)
        {
            var mesh = new MeshGeometry3D();

            // 頂点（底面4点 + 頂点1点）
            mesh.Positions = new Point3DCollection
            {
                new Point3D(0, 0, 0),   // 0: 左下手前
                new Point3D(1, 0, 0),   // 1: 右下手前
                new Point3D(1, 0, 1),   // 2: 右下奥
                new Point3D(0, 0, 1),   // 3: 左下奥
                new Point3D(0.5, 1, 0.5) // 4: 頂点
            };

            // 三角形インデックス（反時計回り）
            mesh.TriangleIndices = new Int32Collection
            {
                // 底面
                0,1,2,
                0,2,3,

                // 側面4枚
                0,4,1,
                1,4,2,
                2,4,3,
                3,4,0
            };

            var material = ObjectMaterialFactory.Create(obj, isGhost, canPlace);

            return new GeometryModel3D(mesh, material)
            {
                BackMaterial = material
            };
        }
    }
}
