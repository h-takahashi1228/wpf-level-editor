using MapEditTool.Models.Enums;
using MapEditTool.Models.Map;
using System.Windows.Media.Media3D;

namespace MapEditTool.Scene3D
{
    public abstract class BlockSceneBase
    {
        public GridPos GridPosition { get; private set; }
        public int Yaw { get; private set; }
        public BlockType Type { get; private set; }
        public bool IsGhost { get; private set; }

        // 3Dモデル
        public Model3D Model { get; protected set; }

        protected BlockSceneBase(GridPos gridPosition, int yaw, BlockType type, bool isGhost)
        {
            GridPosition = gridPosition;
            Yaw = yaw;
            Type = type;
            IsGhost = isGhost;
        }

        // Transformを作成
        protected Transform3D CreateTransform(GridPos pos, int yaw)
        {
            var group = new Transform3DGroup();

            // 回転
            group.Children.Add(
                new RotateTransform3D(
                    new AxisAngleRotation3D(new Vector3D(0, 1, 0), yaw),
                    0.5, 0.5, 0.5
                )
            );

            // 位置
            group.Children.Add(
                new TranslateTransform3D(pos.X, pos.Y, pos.Z)
            );

            return group;
        }

        // Sceneの更新
        public virtual void Update(GridPos pos, int yaw, BlockType type, bool isGhost)
        {
            GridPosition = pos;
            Yaw = yaw;
            Type = type;
            IsGhost = isGhost;

            // Transform更新
            Model.Transform = CreateTransform(pos, yaw);
        }
    }
}
