using MapEditTool.Helpers;
using MapEditTool.Models.Enums;
using MapEditTool.Models.Map.ObjectData;
using MapEditTool.Services;
using MapEditTool.Services.UndoRedo.Objects;
using MapEditTool.ViewModel3D;
using MapEditTool.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MapEditTool.ViewModels
{
    public class EnemySpawnerObjectViewModel
        : ObjectViewModelBase<EnemySpawnerData>
    {
        public ObservableCollection<EnemySpawnEntryViewModel> SpawnEntries { get; }
            = new ObservableCollection<EnemySpawnEntryViewModel>();

        public ICommand AddSpawnEntryCommand { get; }
        public ICommand RemoveSpawnEntryCommand { get; }

        private List<EnemySpawnEntry> _beforeEdit;

        public EnemySpawnerObjectViewModel(
            EnemySpawnerData model,
            LevelData level,
            LevelEditorService service,
            EditorState state,
            CommandManager commandManager,
            Map3DViewModel map3D)
            : base(model, level, service, state, commandManager, map3D)
        {
            AddSpawnEntryCommand = new RelayCommand(_ => AddSpawnEntry());
            RemoveSpawnEntryCommand = new RelayCommand(entry => RemoveSpawnEntry((EnemySpawnEntryViewModel)entry));

            // モデル → ViewModel へ変換
            foreach (var entry in model.EnemySpawnData)
            {
                var vm = new EnemySpawnEntryViewModel(entry);
                vm.Edited += OnEntryEdited;
                SpawnEntries.Add(vm);
            }
        }

        private void BeginEdit()
        {
            // コピーを作る
            _beforeEdit = Model.EnemySpawnData
                .Select(e => new EnemySpawnEntry
                {
                    EnemyType = e.EnemyType,
                    Time = e.Time
                })
                .ToList();
        }

        private void CommitEdit()
        {
            // ViewModel → モデルへ反映
            Model.EnemySpawnData = SpawnEntries
                .Select(vm => new EnemySpawnEntry
                {
                    EnemyType = vm.EnemyType,
                    Time = vm.SpawnTime
                })
                .ToList();

            // Undo/Redo コマンド発行
            var after = Model.EnemySpawnData
                .Select(e => new EnemySpawnEntry
                {
                    EnemyType = e.EnemyType,
                    Time = e.Time
                })
                .ToList();

            _commandManager.Execute(
                new ChangeEnemySpawnerDataCommand(_service, Model, _beforeEdit, after)
            );
        }

        // 追加
        public void AddSpawnEntry()
        {
            BeginEdit();

            var entry = new EnemySpawnEntry
            {
                EnemyType = EnemyType.EnemyA,
                Time = 0f
            };

            var vm = new EnemySpawnEntryViewModel(entry);
            vm.Edited += OnEntryEdited;

            SpawnEntries.Add(vm);

            CommitEdit();
        }

        // 削除
        public void RemoveSpawnEntry(EnemySpawnEntryViewModel entry)
        {
            BeginEdit();

            entry.Edited -= OnEntryEdited;
            SpawnEntries.Remove(entry);

            CommitEdit();
        }

        // 編集
        private void OnEntryEdited(object sender, EventArgs e)
        {
            BeginEdit();
            CommitEdit();
        }
    }
}
