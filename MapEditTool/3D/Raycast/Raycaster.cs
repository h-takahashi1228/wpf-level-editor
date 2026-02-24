using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using MapEditTool.ViewModel3D;

namespace MapEditTool.Raycast3D
{
    public static class Raycaster
    {
        public static RayMeshGeometry3DHitTestResult? Hit(Viewport3D viewport, Point pos, Map3DViewModel map3D)
        {
            RayMeshGeometry3DHitTestResult? result = null;

            VisualTreeHelper.HitTest(
                viewport,
                null,
                r =>
                {
                    if (r is RayMeshGeometry3DHitTestResult ray)
                    {
                        if (ray.ModelHit == map3D.GhostModel || ray.ModelHit == map3D.OutlineModel)
                            return HitTestResultBehavior.Continue;

                        result = ray;
                        return HitTestResultBehavior.Stop;
                    }
                    return HitTestResultBehavior.Continue;
                },
                new PointHitTestParameters(pos)
            );
            return result;
        }
    }
}
