using MapEditTool.Rendering3D;
using MapEditTool.Models.Map;

namespace MapEditTool.Scene3D
{
    public class CubeOutlineScene : BlockOutlineSceneBase
    {
        public CubeOutlineScene(GridPos pos, int yaw)
            : base(pos, yaw)
        {
            var renderer = new CubeOutlineRenderer();
            Model = renderer.Render();

            Model.Transform = CreateTransform(pos, yaw);
        }

        public override void Update(GridPos pos, int yaw)
        {
            base.Update(pos, yaw);
        }
    }
}
