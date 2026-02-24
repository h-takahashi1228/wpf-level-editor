using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;

namespace MapEditTool.Services
{
    public class OutlineState
    {
        public BlockData? BlockOutline { get; set; }
        public ObjectData? ObjectOutline { get; set; }
        public bool IsVisible => BlockOutline != null || ObjectOutline != null;
    }
}
