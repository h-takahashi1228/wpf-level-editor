using System.Windows.Media.Media3D;

namespace MapEditTool.Rendering3D
{
    public class SlopeOutlineRenderer : IBlockOutlineRenderer
    {
        public Model3D Render()
        {
            var points = new[]
            {
                // bottom
                new Point3D(0,0,0), // 0
                new Point3D(1,0,0), // 1
                new Point3D(1,0,1), // 2
                new Point3D(0,0,1), // 3

                // top (slope)
                new Point3D(0,1,1), // 4
                new Point3D(1,1,1), // 5
            };

            var edges = new (int, int)[]
            {
                // bottom
                (0,1),(1,2),(2,3),(3,0),

                // slope top
                (4,5),

                // sides
                (0,3),(3,4),(4,0),
                (1,5),(5,2),(2,1)
            };

            return OutlineGeometryFactory.Create(points, edges);
        }
    }
}