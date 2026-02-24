using MapEditTool.Rendering3D;
using MapEditTool.Models.Map;

namespace MapEditTool.Scene3D
{
    public class SpawnerOutlineScene : ObjectOutlineSceneBase
    {
        public SpawnerOutlineScene(GridPos pos, int yaw)
            : base(pos, yaw)
        {
            var renderer = new SpawnerOutlineRenderer();
            Model = renderer.Render();

            Model.Transform = CreateTransform(pos, yaw);
        }

        public override void Update(GridPos pos, int yaw)
        {
            base.Update(pos, yaw);
        }
    }
}
