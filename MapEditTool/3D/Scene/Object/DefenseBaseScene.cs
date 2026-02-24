using MapEditTool.Rendering3D;
using MapEditTool.Models.Enums;
using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;

namespace MapEditTool.Scene3D
{
    public class DefenseBaseScene : ObjectSceneBase
    {
        public DefenseBaseScene(GridPos gridPosition, int yaw, bool isGhost, bool canPlace)
            : base(gridPosition, yaw, isGhost, canPlace)
        {
            var renderer = ObjectRendererFactory.GetRenderer(ObjectType.DefenseBase);

            Model = renderer.Render(
                new DefenseBaseData { },
                isGhost,
                canPlace
            );

            Model.Transform = CreateTransform(gridPosition, yaw);
        }

        public override void Update(GridPos pos, int yaw, bool isGhost, bool canPlace)
        {
            base.Update(pos, yaw, isGhost, canPlace);
        }
    }
}
