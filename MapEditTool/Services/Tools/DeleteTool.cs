using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;
using MapEditTool.ViewModel3D;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;


namespace MapEditTool.Services.Tools
{
    public class DeleteTool : EditorTool
    {
        public DeleteTool(EditorState state, LevelData level, LevelEditorService service, CommandManager commands, Map3DViewModel map3D)
            : base(state, level, service, commands, map3D) { }

        // DeleteTool選択時
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

            if (hit is not RayMeshGeometry3DHitTestResult meshHit) return;

            if (meshHit.ModelHit is not GeometryModel3D model) return;

            if (map3D.HitRegistry.TryGetBlockData(model, out BlockData block))
            {

                var cmd = new DeleteBlockCommand(Level, block);
                CommandManager.Execute(cmd);
            }
            else if (map3D.HitRegistry.TryGetObjectData(model, out ObjectData obj))
            {

                var cmd = new DeleteObjectCommand(Level, obj);
                CommandManager.Execute(cmd);

                // PlayerSpawnerの配置可能フラグ更新
                if (obj is PlayerSpawnerData)
                    State.CanPlayerSpawnerPlace = true;

                // DefenseBaseの配置可能フラグ更新
                if (obj is DefenseBaseData)
                    State.CanDefenseBasePlace = true;
            }
        }
    }
}
