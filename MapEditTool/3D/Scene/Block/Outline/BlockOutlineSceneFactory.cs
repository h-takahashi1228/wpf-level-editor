using MapEditTool.Models.Enums;
using MapEditTool.Models.Map;

namespace MapEditTool.Scene3D
{
    public static class BlockOutlineSceneFactory
    {
        public static BlockOutlineSceneBase Create(BlockData block)
        {
            return block.Shape switch
            {
                BlockShape.Cube => new CubeOutlineScene(block.Position, block.Yaw),
                BlockShape.Slope => new SlopeOutlineScene(block.Position, block.Yaw),
                _ => throw new NotImplementedException()
            };
        }
    }
}
