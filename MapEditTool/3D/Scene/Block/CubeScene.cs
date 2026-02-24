using MapEditTool.Models.Enums;
using MapEditTool.Models.Map;
using MapEditTool.Rendering3D;
using System.Windows.Media.Media3D;

namespace MapEditTool.Scene3D
{
    public class CubeScene : BlockSceneBase
    {
        public CubeScene(GridPos gridPosition, int yaw, BlockType type, bool isGhost)
            : base(gridPosition, yaw, type, isGhost)
        {
            var renderer = BlockRendererFactory.GetRenderer(BlockShape.Cube);

            Model = renderer.Render(
                new BlockData { Type = type },
                isGhost
            );

            Model.Transform = CreateTransform(gridPosition, yaw);
        }

        public override void Update(GridPos pos, int yaw, BlockType type, bool isGhost)
        {
            base.Update(pos, yaw, type, isGhost);

            ((GeometryModel3D)Model).Material =
                BlockMaterialFactory.Create(type, isGhost);
        }
    }
}
