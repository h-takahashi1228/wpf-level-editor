using MapEditTool.Models.Enums;

namespace MapEditTool.Rendering3D
{
    public class BlockOutlineRendererFactory
    {
        private static readonly Dictionary<BlockShape, IBlockOutlineRenderer> _renderers =
            new()
            {
                { BlockShape.Cube, new CubeOutlineRenderer() },
                { BlockShape.Slope, new SlopeOutlineRenderer() }
            };
        public static IBlockOutlineRenderer GetRenderer(BlockShape shape)
        {
            return _renderers[shape];
        }
    }
}
