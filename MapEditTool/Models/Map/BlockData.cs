using MapEditTool.Models.Enums;

namespace MapEditTool.Models.Map
{
    public class BlockData
    {
        public GridPos Position { get; set; }　// 座標
        public BlockShape Shape { get; set; } // 形状
        public BlockType Type { get; set; } // 材質
        public int Yaw { get; set; } // 向き
    }
}
