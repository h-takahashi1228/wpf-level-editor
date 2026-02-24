using MapEditTool.Models.Enums;
using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;
using MapEditTool.ViewModel3D;
using MapEditTool.ViewModels;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Documents;
using System.Windows.Media.Media3D;

namespace MapEditTool.Services
{
    public class EditorState : INotifyPropertyChanged
    {
        public LevelData Level { get; set; }
        public LevelEditorService LevelEditorService { get; set; }
        public CommandManager CommandManager { get; set; }
        public Map3DViewModel Map3D { get; set; }

        // ViewModel
        public BlockViewModel BlockVM { get; }
        public PlayerSpawnerObjectViewModel PlayerSpawnerObjectVM { get; }
        public EnemySpawnerObjectViewModel EnemySpawnerObjectVM { get; }
        public DefenseBaseObjectViewModel DefenseBaseObjectVM { get; }
        public EmptySelectionViewModel EmptyVM { get; }

        public EditorState(LevelData level, LevelEditorService service, CommandManager cmd, Map3DViewModel map3D)
        {
            Level = level;
            LevelEditorService = service;
            CommandManager = cmd;
            Map3D = map3D;

            BlockVM = new BlockViewModel(Level, LevelEditorService, this, CommandManager, Map3D);

        }


        public event PropertyChangedEventHandler? PropertyChanged;

        // ツール状態
        public ToolType CurrentTool { get; set; } = ToolType.None;

        // ブロック選択状態
        private BlockData? _selectedBlock;
        public BlockData? SelectedBlock
        {
            get => _selectedBlock;
            set
            {
                if (_selectedBlock != value)
                {
                    _selectedBlock = value;
                    OnPropertyChanged(nameof(SelectedBlock));
                    UpdateSelectedViewModel();
                }
            }
        }

        // オブジェクト選択状態
        private ObjectData? _selectedObject;
        public ObjectData? SelectedObject
        {
            get => _selectedObject;
            set
            {
                if (_selectedObject != value)
                {
                    _selectedObject = value;
                    OnPropertyChanged(nameof(SelectedObject));
                    UpdateSelectedViewModel();
                }
            }
        }

        // 選択対象のVM
        private object? _selectedTargetViewModel;
        public object? SelectedTargetViewModel
        {
            get => _selectedTargetViewModel;
            private set
            {
                _selectedTargetViewModel = value;
                OnPropertyChanged(nameof(SelectedTargetViewModel));
            }
        }

        // 選択対象に合わせてViewModelを変更
        private void UpdateSelectedViewModel()
        {
            if (SelectedBlock != null)
            {
                SelectedTargetViewModel = BlockVM;
                return;
            }

            if (SelectedObject != null)
            {
                SelectedTargetViewModel = SelectedObject switch
                {
                    DefenseBaseData defense =>
                        new DefenseBaseObjectViewModel(
                            defense,
                            Level,
                            LevelEditorService,
                            this,
                            CommandManager,
                            Map3D),

                    PlayerSpawnerData player =>
                        new PlayerSpawnerObjectViewModel(
                            player,
                            Level,
                            LevelEditorService,
                            this,
                            CommandManager,
                            Map3D),

                    EnemySpawnerData enemy =>
                        new EnemySpawnerObjectViewModel(
                            enemy,
                            Level,
                            LevelEditorService,
                            this,
                            CommandManager,
                            Map3D),

                    _ => new EmptySelectionViewModel()
                };

                return;
            }

            SelectedTargetViewModel = new EmptySelectionViewModel();
        }

        // ブロック表示要求イベント
        public Action<BlockData>? OnShowBlockRequested;
        public void RequestShowBlock(BlockData block)
        {
            OnShowBlockRequested?.Invoke(block);
        }

        // ブロック非表示要求イベント
        public Action<BlockData>? OnHideBlockRequested;
        public void RequestHideBlock(BlockData block)
        {
            OnHideBlockRequested?.Invoke(block);
        }

        // オブジェクト表示要求イベント
        public Action<ObjectData>? OnShowObjectRequested;
        public void RequestShowObject(ObjectData obj)
        {
            OnShowObjectRequested?.Invoke(obj);
        }

        // オブジェクト非表示要求イベント
        public Action<ObjectData>? OnHideObjectRequested;
        public void RequestHideObject(ObjectData obj)
        {
            OnHideObjectRequested?.Invoke(obj);
        }

        // 配置パラメータ
        public BlockShape? CurrentBlockShape { get; set; } = BlockShape.Cube;
        public BlockType? CurrentBlockType { get; set; } = BlockType.Grass;
        public int CurrentBlockYaw { get; set; } = 0;

        public ObjectType? CurrentObjectType { get; set; } = ObjectType.PlayerSpawner;
        public int CurrentObjectYaw { get; set; } = 0;

        // ゴースト状態
        public GhostState Ghost { get; } = new GhostState();

        // アウトライン状態
        public OutlineState Outline { get; } = new OutlineState();

        // UI 状態
        public bool IsGameSettingsOpen { get; set; }

        // PlayerSpawnerの設置可能状態
        public bool CanPlayerSpawnerPlace { get; set; } = true;

        // DefenseBaseの設置可能状態
        public bool CanDefenseBasePlace { get; set; } = true;

        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
