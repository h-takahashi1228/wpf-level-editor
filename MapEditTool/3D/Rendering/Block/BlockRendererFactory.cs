using MapEditTool.Models.Enums;

namespace MapEditTool.Rendering3D
{
    public static class BlockRendererFactory
    {
        private static readonly Dictionary<BlockShape, IBlockRenderer> _renderers =
            new()
            {
                { BlockShape.Cube, new CubeRenderer() },
                { BlockShape.Slope, new SlopeRenderer() }
            };

        public static IBlockRenderer GetRenderer(BlockShape shape)
        {
            return _renderers[shape];
        }
    }

}
