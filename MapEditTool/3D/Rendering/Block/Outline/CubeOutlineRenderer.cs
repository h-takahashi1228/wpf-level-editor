using System.Windows.Media.Media3D;

namespace MapEditTool.Rendering3D
{
    public class CubeOutlineRenderer : IBlockOutlineRenderer
    {
        public Model3D Render()
        {
            var points = new[]
            {
                // bottom
                new Point3D(0,0,0),
                new Point3D(1,0,0),
                new Point3D(1,0,1),
                new Point3D(0,0,1),

                // top
                new Point3D(0,1,0),
                new Point3D(1,1,0),
                new Point3D(1,1,1),
                new Point3D(0,1,1),
            };

            var edges = new (int, int)[]
            {
                // bottom
                (0,1),(1,2),(2,3),(3,0),
                // top
                (4,5),(5,6),(6,7),(7,4),
                // vertical
                (0,4),(1,5),(2,6),(3,7)
            };

            return OutlineGeometryFactory.Create(points, edges);
        }
    }
}
