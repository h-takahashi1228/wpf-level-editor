using MapEditTool.Models.Enums;
using MapEditTool.Models.Map;

namespace MapEditTool.View3D
{
    public class VisualHitInfo
    {
        public GridPos Position { get; }
        public int Yaw { get; }
        public PlacementKind Kind { get; }
        public VisualHitInfo(GridPos pos, int yaw, PlacementKind kind)
        {
            Position = pos;
            Yaw = yaw;
            Kind = kind;
        }
    }
}