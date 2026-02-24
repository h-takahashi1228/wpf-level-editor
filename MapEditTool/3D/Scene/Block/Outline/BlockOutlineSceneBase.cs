using MapEditTool.Models.Map;
using System.Windows.Media.Media3D;

namespace MapEditTool.Scene3D
{
    public abstract class BlockOutlineSceneBase
    {
        public GridPos GridPosition { get; private set; }
        public int Yaw { get; private set; }

        public Model3D Model { get; protected set; }

        protected BlockOutlineSceneBase(GridPos pos, int yaw)
        {
            GridPosition = pos;
            Yaw = yaw;
        }

        protected Transform3D CreateTransform(GridPos pos, int yaw)
        {
            var group = new Transform3DGroup();

            group.Children.Add(
                new RotateTransform3D(
                    new AxisAngleRotation3D(new Vector3D(0, 1, 0), yaw),
                    0.5, 0.5, 0.5
                )
            );

            group.Children.Add(
                new TranslateTransform3D(pos.X, pos.Y, pos.Z)
            );

            return group;
        }

        public virtual void Update(GridPos pos, int yaw)
        {
            GridPosition = pos;
            Yaw = yaw;

            Model.Transform = CreateTransform(pos, yaw);
        }
    }
}
