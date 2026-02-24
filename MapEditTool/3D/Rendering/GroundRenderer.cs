using MapEditTool.Scene3D;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MapEditTool.Rendering3D
{
    public class GroundRenderer
    {
        private readonly GroundScene _scene;

        public GroundRenderer(GroundScene scene)
        {
            _scene = scene;
        }

        public Model3D Render()
        {
            var group = new Model3DGroup();

            group.Children.Add(CreateGround());
            group.Children.Add(CreateGrid());

            return group;
        }

        // 床
        public GeometryModel3D CreateGround()
        {
            var mesh = new MeshGeometry3D();

            int width = _scene.Width;
            int depth = _scene.Depth;
            //int y = 0;

            mesh.Positions = new Point3DCollection
            {
                new(0,0,0),
                new(width,0,0),
                new(width,0,depth),
                new(0,0,depth)
            };

            mesh.TriangleIndices = new Int32Collection
            {
                2,1,0,
                3,2,0
            };

            var material = new DiffuseMaterial(
                new SolidColorBrush(Color.FromArgb(50, 0, 0, 150)));

            var model = new GeometryModel3D(mesh, material)
            {
                BackMaterial = material
            };

            return new GeometryModel3D(mesh, material);
        }

        // グリッド
        public Model3D CreateGrid()
        {
            var group = new Model3DGroup();
            var material = new DiffuseMaterial(
                new SolidColorBrush(Color.FromRgb(80, 80, 80)));

            int width = _scene.Width;
            int depth = _scene.Depth;
            int y = 0;

            double thickness = 0.02;

            for (int i = 0; i <= width; i++)
            {
                double pos = i * _scene.GridStep;

                group.Children.Add(CreateLine(
                    new Point3D(0, 0.01, pos),
                    new Point3D(width, 0.01, pos),
                    thickness,
                    material));

                group.Children.Add(CreateLine(
                    new Point3D(pos, 0.01, 0),
                    new Point3D(pos, 0.01, width),
                    thickness,
                    material));
            }

            return group;
        }

        // 線
        static GeometryModel3D CreateLine(
            Point3D start,
            Point3D end,
            double thickness,
            Material material)
        {
            Vector3D dir = end - start;
            dir.Normalize();

            Vector3D up = new(0, 1, 0);
            Vector3D right = Vector3D.CrossProduct(dir, up) * thickness;

            var mesh = new MeshGeometry3D
            {
                Positions = new Point3DCollection
                {
                    start - right,
                    start + right,
                    end + right,
                    end - right
                },
                TriangleIndices = new Int32Collection
                {
                    0,1,2,
                    0,2,3
                }
            };

            return new GeometryModel3D(mesh, material)
            {
                BackMaterial = material
            };
        }
    }
}
