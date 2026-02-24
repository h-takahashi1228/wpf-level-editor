using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;

namespace MapEditTool.Services
{
    public class GhostState
    {
        public BlockData? BlockGhost { get; set; }
        public ObjectData? ObjectGhost { get; set; }
        public bool IsVisible => BlockGhost != null || ObjectGhost != null;
    }

}
