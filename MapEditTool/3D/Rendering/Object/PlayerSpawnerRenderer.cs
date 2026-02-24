using MapEditTool.Models.Map.ObjectData;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MapEditTool.Rendering3D
{
    public class PlayerSpawnerRenderer : IObjectRenderer
    {
        public Model3D Render(ObjectData obj, bool isGhost = false, bool canPlace = true)
        {
            var mesh = new MeshGeometry3D();

            // 横幅（細さ）
            double w = 0.3;

            // 頂点（上1・中4・下1）
            mesh.Positions = new Point3DCollection
            {
                new Point3D(0.5, 1.0, 0.5),     // 0: 上の頂点

                new Point3D(0.5 - w, 0.5, 0.5), // 1: 中央 左
                new Point3D(0.5 + w, 0.5, 0.5), // 2: 中央 右
                new Point3D(0.5, 0.5, 0.5 - w), // 3: 中央 前
                new Point3D(0.5, 0.5, 0.5 + w), // 4: 中央 後

                new Point3D(0.5, 0.0, 0.5),     // 5: 下の頂点
            };

            // 三角形インデックス（8 面）
            mesh.TriangleIndices = new Int32Collection
            {
                // 上側 4 面
                0, 1, 3,
                0, 3, 2,
                0, 2, 4,
                0, 4, 1,

                // 下側 4 面
                5, 3, 1,
                5, 2, 3,
                5, 4, 2,
                5, 1, 4
            };

            var material = ObjectMaterialFactory.Create(obj, isGhost, canPlace);

            return new GeometryModel3D(mesh, material);
        }
    }
}
