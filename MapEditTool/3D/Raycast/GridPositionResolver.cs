using System.Windows.Media.Media3D;
using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;
using MapEditTool.View3D;

namespace MapEditTool.Raycast3D
{
    public static class GridPositionResolver
    {
        public static GridPos Resolve(LevelData level, VisualHitRegistry visualHitRegistry, RayMeshGeometry3DHitTestResult ray)
        {
            if (ray is not RayMeshGeometry3DHitTestResult meshHit)
                return new GridPos();

            if (meshHit.ModelHit is not GeometryModel3D model)
                return new GridPos();

            // 既存ブロックにヒットした場合
            if (visualHitRegistry.TryGetBlockData(model, out BlockData blockData))
            {
                // 法線計算
                Vector3D normal = CalculateNormal(ray);

                // ブロックの向き補正
                Vector3D rotatedNormal = RotateNormal(normal, blockData.Yaw);

                // どの方向に置くか決定
                Vector3D axis = GetDominantAxis(ray.PointHit, rotatedNormal);

                return new GridPos(
                    blockData.Position.X + Math.Sign(axis.X),
                    blockData.Position.Y + Math.Sign(axis.Y),
                    blockData.Position.Z + Math.Sign(axis.Z)
                );
            }
            // 既存オブジェクトにヒットした場合
            else if(visualHitRegistry.TryGetObjectData(model, out ObjectData objectData))
            {
                // 法線計算
                Vector3D normal = CalculateNormal(ray);

                // ブロックの向き補正
                Vector3D rotatedNormal = RotateNormal(normal, objectData.Yaw);

                // どの方向に置くか決定
                Vector3D axis = GetDominantAxis(ray.PointHit, rotatedNormal);

                return new GridPos(
                    objectData.Position.X + Math.Sign(axis.X),
                    objectData.Position.Y + Math.Sign(axis.Y),
                    objectData.Position.Z + Math.Sign(axis.Z)
                );
            }

            // 既存ブロック/オブジェクトにヒットしなかった場合
            var p = GetGrid(ray.PointHit);
            return new GridPos(p.x, 0, p.z);
        }

        private static (int x, int z) GetGrid(Point3D p)
            => ((int)Math.Floor(p.X), (int)Math.Floor(p.Z));

        private static Vector3D CalculateNormal(RayMeshGeometry3DHitTestResult ray)
        {
            var m = ray.MeshHit;
            var p0 = m.Positions[ray.VertexIndex1];
            var p1 = m.Positions[ray.VertexIndex2];
            var p2 = m.Positions[ray.VertexIndex3];
            var n = Vector3D.CrossProduct(p1 - p0, p2 - p0);
            n.Normalize();
            return n;
        }

        private static Vector3D RotateNormal(Vector3D n, int yaw)
        {
            return yaw switch
            {
                90 => new Vector3D(n.Z, n.Y, -n.X),
                180 => new Vector3D(-n.X, n.Y, -n.Z),
                270 => new Vector3D(-n.Z, n.Y, n.X),
                _ => n
            };
        }

        private static Vector3D GetDominantAxis(Point3D hit, Vector3D normal)
        {
            double fx = hit.X - Math.Floor(hit.X);
            double fy = hit.Y - Math.Floor(hit.Y);
            double fz = hit.Z - Math.Floor(hit.Z);

            double sx = (normal.X >= 0 ? fx : 1 - fx) + Math.Abs(normal.X);
            double sy = (normal.Y >= 0 ? fy : 1 - fy) + Math.Abs(normal.Y);
            double sz = (normal.Z >= 0 ? fz : 1 - fz) + Math.Abs(normal.Z);

            if (sy >= sx && sy >= sz)
                return new Vector3D(0, Math.Sign(normal.Y), 0);

            if (sx >= sz)
                return new Vector3D(Math.Sign(normal.X), 0, 0);

            return new Vector3D(0, 0, Math.Sign(normal.Z));
        }
    }
}
