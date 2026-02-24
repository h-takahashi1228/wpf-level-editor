using MapEditTool.Raycast3D;
using MapEditTool.ViewModel3D;
using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace MapEditTool.Services.Tools
{
    public class SelectTool : EditorTool
    {
        private bool _isDragging = false;
        public EditorState UiState => State;

        public SelectTool(EditorState state, LevelData level, LevelEditorService service, CommandManager commands, Map3DViewModel map3D)
            : base(state, level, service, commands, map3D)
        {
        }

        // SelectTool選択時
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
            Mouse.Capture(viewport);

            // ブロック以外がクリックされた場合
            if (hit == null ||
                hit is not RayMeshGeometry3DHitTestResult meshHit ||
                meshHit.ModelHit is not GeometryModel3D model)
            {
                // 選択解除処理
                ClearSelectionAndOutline(map3D);
                return;
            }

            // ブロックをクリックした場合
            if (map3D.HitRegistry.TryGetBlockData(model, out BlockData block))
            {
                State.SelectedObject = null;
                State.Outline.ObjectOutline = null;
                State.RequestHideBlock(block);

                // すでに同じブロックを選択しているなら何もしない
                if (State.SelectedBlock != block)
                {
                    State.SelectedBlock = block;
                }

                State.Ghost.BlockGhost = block;
                map3D.UpdateGhost(State.Ghost);

                State.Outline.BlockOutline = block;
                map3D.UpdateOutline(State.Outline);

                return;
            }
            // オブジェクトをクリックした場合
            else if (map3D.HitRegistry.TryGetObjectData(model, out ObjectData obj))
            {
                State.SelectedBlock = null;
                State.Outline.BlockOutline = null;
                State.RequestHideObject(obj);

                // すでに同じブロックを選択しているなら何もしない
                if (State.SelectedObject != obj)
                {
                    State.SelectedObject = obj;
                }

                // PlayerSpawnerの配置可能フラグ更新
                if (obj is PlayerSpawnerData)
                    State.CanPlayerSpawnerPlace = true;

                // DefenseBaseの配置可能フラグ更新
                if (obj is DefenseBaseData)
                    State.CanDefenseBasePlace = true;

                State.Ghost.ObjectGhost = obj;
                map3D.UpdateGhost(State.Ghost);

                State.Outline.ObjectOutline = obj;
                map3D.UpdateOutline(State.Outline);

                return;
            }

            // ブロック/オブジェクト以外をクリックした場合 → 選択解除
            ClearSelectionAndOutline(map3D);
        }

        // マウス移動時
        public override void OnMouseMove(Viewport3D viewport, Point mousePos, RayMeshGeometry3DHitTestResult? hit, Map3DViewModel map3D)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                _isDragging = true;

            if (!_isDragging) return;
            if (hit == null) return;

            if (State.SelectedBlock != null) { 
                var pos = GridPositionResolver.Resolve(Level, map3D.HitRegistry, hit);

                if (pos != State.SelectedBlock.Position && Level.IsOccupied(pos))
                    return;

                BlockData block = new BlockData
                {
                    Position = pos,
                    Shape = State.SelectedBlock.Shape,
                    Type = State.SelectedBlock!.Type,
                    Yaw = State.SelectedBlock.Yaw
                };

                State.Ghost.BlockGhost = block;
                State.Outline.BlockOutline = block;
            }
            else if(State.SelectedObject != null) {
                var pos = GridPositionResolver.Resolve(Level, map3D.HitRegistry, hit);

                if (pos != State.SelectedObject.Position && Level.IsOccupied(pos))
                    return;

                var obj = State.SelectedObject.Clone();
                obj.Position = pos;

                State.Ghost.ObjectGhost = obj;
                State.Outline.ObjectOutline = obj;
            }

            map3D.UpdateGhost(State.Ghost);
            map3D.UpdateOutline(State.Outline);
        }

        // 左クリック解除時
        public override void OnLeftRelease(Viewport3D viewport, Point mousePos, RayMeshGeometry3DHitTestResult? hit, Map3DViewModel map3D)
        {
            bool wasDragging = _isDragging;
            _isDragging = false;
            ReleaseCapture(viewport);

            if (State.SelectedBlock == null && State.SelectedObject == null)
                return;

            // ゴーストを消す
            State.Ghost.BlockGhost = null;
            State.Ghost.ObjectGhost = null;
            map3D.UpdateGhost(State.Ghost);

            // オブジェクトの場合は配置可能フラグを更新
            if (State.SelectedObject != null)
            {
                // PlayerSpawnerの配置可能フラグ更新
                if (State.SelectedObject is PlayerSpawnerData)
                    State.CanPlayerSpawnerPlace = false;
                // DefenseBaseの配置可能フラグ更新
                if (State.SelectedObject is DefenseBaseData)
                    State.CanDefenseBasePlace = false;
            }

            // ドラッグしていなかった場合は元の位置に戻す
            if (!wasDragging)
            {
                if (State.SelectedBlock != null)
                {
                    State.RequestShowBlock(State.SelectedBlock);
                    State.Outline.BlockOutline = State.SelectedBlock;
                    map3D.UpdateOutline(State.Outline);
                    return;
                }
                else if (State.SelectedObject != null)
                {
                    State.RequestShowObject(State.SelectedObject);
                    State.Outline.ObjectOutline = State.SelectedObject;
                    map3D.UpdateOutline(State.Outline);
                    return;
                }
            }

            if (hit == null)
            {
                if (State.SelectedBlock != null)
                {
                    State.RequestShowBlock(State.SelectedBlock);
                    State.Outline.BlockOutline = State.SelectedBlock;
                    map3D.UpdateOutline(State.Outline);
                    return;
                }
                else if (State.SelectedObject != null)
                {
                    State.RequestShowObject(State.SelectedObject);
                    State.Outline.ObjectOutline = State.SelectedObject;
                    map3D.UpdateOutline(State.Outline);
                    return;
                }
            }

            GridPos pos = GridPositionResolver.Resolve(Level, map3D.HitRegistry, hit);

            // 元の位置以外で占有されていたら移動不可
            if (State.SelectedBlock != null)
            {
                // 元の位置と同じなら MoveBlockCommand を実行しない
                if (pos == State.SelectedBlock.Position)
                {
                    // 元のオブジェクトを再表示
                    State.RequestShowBlock(State.SelectedBlock);

                    // アウトラインを元に戻す
                    State.Outline.BlockOutline = State.SelectedBlock;
                    map3D.UpdateOutline(State.Outline);

                    // ゴーストを消す
                    State.Ghost.BlockGhost = null;
                    map3D.UpdateGhost(State.Ghost);

                    return;
                }

                // 元の位置以外で占有されていたら移動不可
                if (Level.IsOccupied(pos))
                {
                    State.RequestShowBlock(State.SelectedBlock);

                    State.Outline.BlockOutline = State.SelectedBlock;
                    map3D.UpdateOutline(State.Outline);

                    State.Ghost.BlockGhost = null;
                    map3D.UpdateGhost(State.Ghost);

                    return;
                }

                var cmd = new MoveBlockCommand(Service, State.SelectedBlock, State.SelectedBlock.Position, pos);
                CommandManager.Execute(cmd);

                var movedBlock = State.SelectedBlock;
                State.SelectedBlock = null;
                State.SelectedBlock = movedBlock;
            }
            else if (State.SelectedObject != null)
            {
                // 元の位置と同じなら MoveObjectCommand を実行しない
                if (pos == State.SelectedObject.Position)
                {
                    // 元のオブジェクトを再表示
                    State.RequestShowObject(State.SelectedObject);

                    // アウトラインを元に戻す
                    State.Outline.ObjectOutline = State.SelectedObject;
                    map3D.UpdateOutline(State.Outline);

                    // ゴーストを消す
                    State.Ghost.ObjectGhost = null;
                    map3D.UpdateGhost(State.Ghost);

                    return;
                }

                // 元の位置以外で占有されていたら移動不可
                if (Level.IsOccupied(pos))
                {
                    State.RequestShowObject(State.SelectedObject);

                    State.Outline.ObjectOutline = State.SelectedObject;
                    map3D.UpdateOutline(State.Outline);

                    State.Ghost.ObjectGhost = null;
                    map3D.UpdateGhost(State.Ghost);

                    return;
                }

                var cmd = new MoveObjectCommand(Service, State.SelectedObject, State.SelectedObject.Position, pos);
                CommandManager.Execute(cmd);

                // 選択状態を更新
                var movedObject = State.SelectedObject;
                State.SelectedObject = null;
                State.SelectedObject = movedObject;
            }


        }

        // 選択状態の解除
        private void ClearSelectionAndOutline(Map3DViewModel map3D)
        {
            if (State.SelectedBlock != null)
            {
                State.SelectedBlock = null;
                State.Outline.BlockOutline = null;
            }

            if (State.SelectedObject != null)
            {
                State.SelectedObject = null;
                State.Outline.ObjectOutline = null;
            }

            map3D.UpdateOutline(State.Outline);
        }

        private void ReleaseCapture(Viewport3D viewport)
        {
            if (Mouse.Captured == viewport)
                Mouse.Capture(null);
        }
    }
}
