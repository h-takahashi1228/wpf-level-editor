using MapEditTool.Raycast3D;
using MapEditTool.ViewModel3D;
using MapEditTool.Models.Enums;
using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;


namespace MapEditTool.Services.Tools
{
    public class PlaceBlockTool : EditorTool
    {
        public PlaceBlockTool(EditorState state, LevelData level, LevelEditorService service, CommandManager commands, Map3DViewModel map3D)
            : base(state, level, service, commands, map3D) { }

        public BlockShape? CurrentBlockShape
        {
            get => State.CurrentBlockShape;
            set => State.CurrentBlockShape = value;
        }
        public BlockType? CurrentBlockType
        {
            get => State.CurrentBlockType;
            set => State.CurrentBlockType = value;
        }
        public int CurrentBlockYaw
        {
            get => State.CurrentBlockYaw;
            set => State.CurrentBlockYaw = value;
        }

        // PlaceBlockTool選択時
        public override void OnActivated()
        {
            State.Ghost.BlockGhost = null;
            State.Ghost.ObjectGhost = null;
            State.Outline.BlockOutline = null;
            State.Outline.ObjectOutline = null;
        }

        // 左クリック時
        public override void OnLeftClick(Viewport3D viewport, Point mousePos, RayMeshGeometry3DHitTestResult? hit, Map3DViewModel map3D)
        {
            if (hit == null) return;

            GridPos pos = GridPositionResolver.Resolve(Level, map3D.HitRegistry, hit);

            if (Level.IsOccupied(pos))
                return;

            var block = new BlockData
            {
                Position = pos,
                Shape = State.CurrentBlockShape!.Value,
                Type = State.CurrentBlockType!.Value,
                Yaw = State.CurrentBlockYaw
            };

            var cmd = new PlaceBlockCommand(Level, block);
            CommandManager.Execute(cmd);
        }

        // マウス移動時
        public override void OnMouseMove(Viewport3D viewport, Point mousePos, RayMeshGeometry3DHitTestResult? hit, Map3DViewModel map3D)
        {
            if (hit == null) return;

            var pos = GridPositionResolver.Resolve(Level, map3D.HitRegistry, hit);

            if (Level.IsOccupied(pos))
            {
                State.Ghost.BlockGhost = null;
                return;
            }

            State.Ghost.BlockGhost = new BlockData
            {
                Position = pos,
                Shape = State.CurrentBlockShape!.Value,
                Type = State.CurrentBlockType!.Value,
                Yaw = State.CurrentBlockYaw
            };

            map3D.UpdateGhost(State.Ghost);
        }
    }
}
