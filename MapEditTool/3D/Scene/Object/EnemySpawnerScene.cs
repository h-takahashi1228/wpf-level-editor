using MapEditTool.Rendering3D;
using MapEditTool.Models.Enums;
using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;

namespace MapEditTool.Scene3D
{
    public class EnemySpawnerScene : ObjectSceneBase
    {
        public EnemySpawnerScene(GridPos gridPosition, int yaw, bool isGhost, bool canPlace)
            : base(gridPosition, yaw, isGhost, canPlace)
        {
            var renderer = ObjectRendererFactory.GetRenderer(ObjectType.EnemySpawner);

            Model = renderer.Render(
                new EnemySpawnerData { },
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
