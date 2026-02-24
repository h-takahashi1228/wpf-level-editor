using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MapEditTool.Rendering3D
{
    public static class OutlineGeometryFactory
    {
        public static GeometryModel3D Create(
            IReadOnlyList<Point3D> points,
            IReadOnlyList<(int a, int b)> edges)
        {
            Color color = Colors.Red;
            double thickness = 0.03;
            var mesh = new MeshGeometry3D();

            // 元の頂点（AddEdge 内で index を使うため）
            foreach (var p in points)
                mesh.Positions.Add(p);

            foreach (var (a, b) in edges)
                AddEdge(mesh, points[a], points[b], thickness);

            var group = new MaterialGroup();
            group.Children.Add(new DiffuseMaterial(new SolidColorBrush(color)));
            group.Children.Add(new EmissiveMaterial(new SolidColorBrush(color)));


            return new GeometryModel3D(mesh, group)
            {
                BackMaterial = group
            };
        }
        static void AddEdge(
            MeshGeometry3D mesh,
            Point3D a,
            Point3D b,
            double thickness)
        {
            Vector3D dir = b - a;
            dir.Normalize();

            // dir とほぼ平行でない up を選ぶ
            Vector3D up = Math.Abs(Vector3D.DotProduct(dir, new Vector3D(0, 1, 0))) > 0.9
                ? new Vector3D(1, 0, 0)
                : new Vector3D(0, 1, 0);

            Vector3D side = Vector3D.CrossProduct(dir, up);
            side.Normalize();
            side *= thickness;

            int start = mesh.Positions.Count;

            mesh.Positions.Add(a + side);
            mesh.Positions.Add(a - side);
            mesh.Positions.Add(b - side);
            mesh.Positions.Add(b + side);

            mesh.TriangleIndices.Add(start);
            mesh.TriangleIndices.Add(start + 1);
            mesh.TriangleIndices.Add(start + 2);

            mesh.TriangleIndices.Add(start);
            mesh.TriangleIndices.Add(start + 2);
            mesh.TriangleIndices.Add(start + 3);
        }
    }
}
