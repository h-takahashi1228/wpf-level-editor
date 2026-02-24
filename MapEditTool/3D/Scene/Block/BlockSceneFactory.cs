using MapEditTool.Models.Enums;
using MapEditTool.Models.Map;

namespace MapEditTool.Scene3D
{
    public static class BlockSceneFactory
    {
        public static BlockSceneBase Create(BlockData block, bool isGhost = false)
        {
            return block.Shape switch
            {
                BlockShape.Cube => new CubeScene(block.Position, block.Yaw, block.Type, isGhost),
                BlockShape.Slope => new SlopeScene(block.Position, block.Yaw, block.Type, isGhost),
                _ => throw new NotImplementedException()
            };
        }
    }
}