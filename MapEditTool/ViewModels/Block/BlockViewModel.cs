using MapEditTool.Models.Enums;
using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;
using MapEditTool.Services;
using MapEditTool.ViewModel3D;
using System.Diagnostics;

namespace MapEditTool.ViewModels
{
    public class BlockViewModel : ViewModelBase
    {
        private readonly LevelData _level;
        private readonly LevelEditorService _service;
        private readonly EditorState _state;
        private readonly CommandManager _commandManager;
        private readonly Map3DViewModel _map3D;

        public BlockViewModel(LevelData level, LevelEditorService service, EditorState state, CommandManager commandManager, Map3DViewModel map3D)
        {
            _level = level;
            _service = service;
            _state = state;
            _commandManager = commandManager;
            _map3D = map3D;

            _state.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(EditorState.SelectedBlock))
                    Block = _state.SelectedBlock;
            };
        }

        private BlockData? _block;
        public BlockData? Block
        {
            get => _block;
            private set
            {
                _block = value;
                PositionError = false;
                Refresh();
            }
        }

        // X座標
        public int X
        {
            get => Block?.Position.X ?? 0;
            set
            {
                if (Block == null || Block.Position.X == value) return;

                var oldPos = Block.Position;
                var newPos = new GridPos(value, oldPos.Y, oldPos.Z);

                if (_level.IsOccupied(newPos))
                {
                    // 移動できないのでUIを元に戻す
                    PositionError = true;
                    Refresh();
                    return; 
                }

                _commandManager.Execute(new MoveBlockCommand(_service, Block, oldPos, newPos));

                // Outline更新
                if (_state.SelectedBlock == Block)
                {
                    _state.Outline.BlockOutline = Block;
                    _map3D.UpdateOutline(_state.Outline);
                }

                Refresh();

            }
        }

        // Y座標
        public int Y
        {
            get => Block?.Position.Y ?? 0;
            set
            {
                if (Block == null || Block.Position.Y == value) return;

                var oldPos = Block.Position;
                var newPos = new GridPos(oldPos.X, value, oldPos.Z);

                if (_level.IsOccupied(newPos))
                {
                    // 移動できないのでUIを元に戻す
                    PositionError = true;
                    Refresh();
                    return;
                }

                _commandManager.Execute(new MoveBlockCommand(_service, Block, oldPos, newPos));

                // Outline更新
                if (_state.SelectedBlock == Block)
                {
                    _state.Outline.BlockOutline = Block;
                    _map3D.UpdateOutline(_state.Outline);
                }

                Refresh();

            }
        }

        // Z座標
        public int Z
        {
            get => Block?.Position.Z ?? 0;
            set
            {
                if (Block == null || Block.Position.Z == value) return;

                var oldPos = Block.Position;
                var newPos = new GridPos(oldPos.X, oldPos.Y, value);

                if (_level.IsOccupied(newPos))
                {
                    // 移動できないのでUIを元に戻す
                    PositionError = true;
                    Refresh();
                    return;
                }

                _commandManager.Execute(new MoveBlockCommand(_service, Block, oldPos, newPos));

                // Outline更新
                if (_state.SelectedBlock == Block)
                {
                    _state.Outline.BlockOutline = Block;
                    _map3D.UpdateOutline(_state.Outline);
                }

                Refresh();

            }
        }
        private bool _positionError; 
        public bool PositionError
        {
            get => _positionError; 
            private set
            {
                _positionError = value;
                RaisePropertyChanged(nameof(PositionError));
            }
        }

        // Yaw
        public int Yaw
        {
            get => Block?.Yaw ?? 0;
            set
            {
                if (Block == null || Block.Yaw == value) return;

                _commandManager.Execute(
                    new ChangeBlockYawCommand(_service, Block, Block.Yaw, value)
                );

                // Outline更新
                if (_state.SelectedBlock == Block)
                {
                    _state.Outline.BlockOutline = Block;
                    _map3D.UpdateOutline(_state.Outline);
                }

                Refresh();

            }
        }

        // Shape
        public BlockShape Shape
        {
            get => Block?.Shape ?? BlockShape.Cube;
            set
            {
                if (Block == null || Block.Shape == value) return;

                _commandManager.Execute(
                    new ChangeBlockShapeCommand(_service, Block, Block.Shape, value)
                );

                // Outline更新
                if (_state.SelectedBlock == Block)
                {
                    _state.Outline.BlockOutline = Block;
                    _map3D.UpdateOutline(_state.Outline);
                }

                Refresh();
            }
        }


        // Type
        public BlockType Type
        {
            get => Block?.Type ?? BlockType.Grass;
            set
            {
                if (Block == null || Block.Type == value) return;

                _commandManager.Execute(
                    new ChangeBlockTypeCommand(_service, Block, Block.Type, value)
                );

                Refresh();

            }
        }

        // 選択中のブロックが変わったら UI を更新
        public void Refresh()
        {
            RaisePropertyChanged(nameof(Block));
            RaisePropertyChanged(nameof(X));
            RaisePropertyChanged(nameof(Y));
            RaisePropertyChanged(nameof(Z));
            RaisePropertyChanged(nameof(Yaw));
            RaisePropertyChanged(nameof(Shape));
            RaisePropertyChanged(nameof(Type));
        }
    }
}