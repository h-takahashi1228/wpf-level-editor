using System.Windows.Media.Media3D;

namespace MapEditTool.Rendering3D
{
    public class SpawnerOutlineRenderer : IObjectOutlineRenderer
    {
        public Model3D Render()
        {
            double w = 0.3;

            var points = new[]
            {
                new Point3D(0.5, 1.0, 0.5),     // 0: 上の頂点

                new Point3D(0.5 - w, 0.5, 0.5), // 1: 中央 左
                new Point3D(0.5 + w, 0.5, 0.5), // 2: 中央 右
                new Point3D(0.5, 0.5, 0.5 - w), // 3: 中央 前
                new Point3D(0.5, 0.5, 0.5 + w), // 4: 中央 後

                new Point3D(0.5, 0.0, 0.5),     // 5: 下の頂点
            };

            var edges = new (int, int)[]
            {
                // 上 → 中央
                (0, 1),
                (0, 2),
                (0, 3),
                (0, 4),

                // 中央 → 下
                (1, 5),
                (2, 5),
                (3, 5),
                (4, 5),
            };

            return OutlineGeometryFactory.Create(points, edges);
        }
    }
}
