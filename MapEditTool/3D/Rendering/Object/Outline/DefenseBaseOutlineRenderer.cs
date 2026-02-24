using System.Windows.Media.Media3D;

namespace MapEditTool.Rendering3D
{
    public class DefenseBaseOutlineRenderer : IObjectOutlineRenderer
    {
        public Model3D Render()
        {
            var p = new[]
            {
                new Point3D(0, 0, 0),   // 0: 左下手前
                new Point3D(1, 0, 0),   // 1: 右下手前
                new Point3D(1, 0, 1),   // 2: 右下奥
                new Point3D(0, 0, 1),   // 3: 左下奥
                new Point3D(0.5, 1, 0.5) // 4: 頂点
            };

            var edges = new (int, int)[]
            {
                // 底面
                (0,1),(1,2),(2,3),(3,0),

                // 側面
                (0,4),(1,4),(2,4),(3,4)
            };

            return OutlineGeometryFactory.Create(p, edges);
        }
    }
}
