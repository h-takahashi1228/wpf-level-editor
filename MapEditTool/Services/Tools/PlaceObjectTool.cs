using MapEditTool.Raycast3D;
using MapEditTool.ViewModel3D;
using MapEditTool.Models.Enums;
using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;


namespace MapEditTool.Services.Tools
{
    public class PlaceObjectTool : EditorTool
    {
        public PlaceObjectTool(EditorState state, LevelData level, LevelEditorService service, CommandManager commands, Map3DViewModel map3D)
            : base(state, level, service, commands, map3D) { }

        public ObjectType? CurrentObjectType
        {
            get => State.CurrentObjectType;
            set
            {
                if (State.CurrentObjectType != value)
                {
                    State.CurrentObjectType = value;
                }
            }
        }
        public int CurrentObjectYaw
        {
            get => State.CurrentObjectYaw;
            set => State.CurrentObjectYaw = value;
        }

        // PlaceObjectTool選択時
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

            // PlayerSpawnerの配置可能チェック
            if (State.CurrentObjectType!.Value == ObjectType.PlayerSpawner && !State.CanPlayerSpawnerPlace)
                return;
            // DefenseBaseの配置可能チェック
            if (State.CurrentObjectType!.Value == ObjectType.DefenseBase && !State.CanDefenseBasePlace)
                return;

            var obj = ObjectDataFactory.Create(
                State.CurrentObjectType!.Value,
                pos,
                State.CurrentObjectYaw
            );

            var cmd = new PlaceObjectCommand(Level, obj);
            CommandManager.Execute(cmd);

            // PlayerSpawnerの配置可能フラグ更新
            if (State.CurrentObjectType!.Value == ObjectType.PlayerSpawner)
                State.CanPlayerSpawnerPlace = false;

            // DefenseBaseの配置可能フラグ更新
            if (State.CurrentObjectType!.Value == ObjectType.DefenseBase)
                State.CanDefenseBasePlace = false;
        }

        // マウス移動時
        public override void OnMouseMove(Viewport3D viewport, Point mousePos, RayMeshGeometry3DHitTestResult? hit, Map3DViewModel map3D)
        {
            if (hit == null) return;

            var pos = GridPositionResolver.Resolve(Level, map3D.HitRegistry, hit);

            if (Level.IsOccupied(pos))
            {
                State.Ghost.ObjectGhost = null;
                return;
            }

            State.Ghost.ObjectGhost = ObjectDataFactory.Create(
                State.CurrentObjectType!.Value,
                pos,
                State.CurrentObjectYaw
            );

            map3D.UpdateGhost(State.Ghost);
        }
    }
}
