using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;
using MapEditTool.Services;
using MapEditTool.ViewModel3D;
using MapEditTool.ViewModels;
using System.Windows.Documents;

namespace MapEditTool.ViewModels
{
    public abstract class ObjectViewModelBase<T> : ViewModelBase
        where T : ObjectData
    {
        protected readonly LevelData _level;
        protected readonly LevelEditorService _service;
        protected readonly EditorState _state;
        protected readonly CommandManager _commandManager;
        protected readonly Map3DViewModel _map3D;

        protected ObjectViewModelBase(
            T model,
            LevelData level,
            LevelEditorService service,
            EditorState state,
            CommandManager commandManager,
            Map3DViewModel map3D)
        {
            Model = model;
            _level = level;
            _service = service;
            _state = state;
            _commandManager = commandManager;
            _map3D = map3D;
        }

        public T Model { get; }

        #region 共通プロパティ

        // X座標
        public int X
        {
            get => Model.Position.X;
            set
            {
                if (Model.Position.X == value) return;

                var oldPos = Model.Position;
                var newPos = new GridPos(value, oldPos.Y, oldPos.Z);

                if (_level.IsOccupied(newPos))
                {
                    // 移動できないのでUIを元に戻す
                    PositionError = true;
                    Refresh();
                    return;
                }

                _commandManager.Execute(
                    new MoveObjectCommand(_service, Model, oldPos, newPos)
                );

                // Outline更新
                _state.Outline.ObjectOutline = _state.SelectedObject;
                _map3D.UpdateOutline(_state.Outline);

                Refresh();
            }
        }

        // Y座標
        public int Y
        {
            get => Model.Position.Y;
            set
            {
                if (Model.Position.Y == value) return;

                var oldPos = Model.Position;
                var newPos = new GridPos(oldPos.X, value, oldPos.Z);

                if (_level.IsOccupied(newPos))
                {
                    // 移動できないのでUIを元に戻す
                    PositionError = true;
                    Refresh();
                    return;
                }

                _commandManager.Execute(
                    new MoveObjectCommand(_service, Model, oldPos, newPos)
                );

                // Outline更新
                _state.Outline.ObjectOutline = _state.SelectedObject;
                _map3D.UpdateOutline(_state.Outline);

                Refresh();
            }
        }

        // Z座標
        public int Z
        {
            get => Model.Position.Z;
            set
            {
                if (Model.Position.Z == value) return;

                var oldPos = Model.Position;
                var newPos = new GridPos(oldPos.X, oldPos.Y, value);

                if (_level.IsOccupied(newPos))
                {
                    // 移動できないのでUIを元に戻す
                    PositionError = true;
                    Refresh();
                    return;
                }

                _commandManager.Execute(
                    new MoveObjectCommand(_service, Model, oldPos, newPos)
                );

                // Outline更新
                _state.Outline.ObjectOutline = _state.SelectedObject;
                _map3D.UpdateOutline(_state.Outline);

                Refresh();
            }
        }

        // 同一ポジションエラー
        private bool _positionError; public bool PositionError
        {
            get => _positionError; private set
            {
                _positionError = value; RaisePropertyChanged(nameof(PositionError));
            }
        }

        #endregion

        // UI更新
        public virtual void Refresh()
        {
            RaisePropertyChanged(nameof(X));
            RaisePropertyChanged(nameof(Y));
            RaisePropertyChanged(nameof(Z));
        }
    }
}