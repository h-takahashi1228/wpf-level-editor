using MapEditTool.Helpers;
using MapEditTool.Models.Map.ObjectData;
using MapEditTool.Raycast3D;
using MapEditTool.Services;
using MapEditTool.Services.SaveLoad;
using MapEditTool.Services.Tools;
using MapEditTool.ViewModel3D;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MapEditTool.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // 子 ViewModel
        public MapViewModel Map { get; set; }
        public Map3DViewModel Map3D { get; set; }

        // 現在のツール
        private EditorTool? _currentTool;
        public EditorTool? CurrentTool
        {
            get => _currentTool;
            set
            {
                _currentTool = value;
                RaisePropertyChanged();
            }
        }

        private readonly LevelSaveService _saveService = new();

        // コマンド
        public ICommand SetPlaceBlockToolCommand { get; }
        public ICommand SetPlaceObjectToolCommand { get; }
        public ICommand SetDeleteToolCommand { get; }
        public ICommand SetSelectToolCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand CameraResetCommand { get; }


        public MainViewModel()
        {
            // ゲームデータ系
            var level = new LevelData();
            var service = new LevelEditorService(level);
            var commands = new CommandManager();
            Map3D = new Map3DViewModel(level, this);
            var state = new EditorState(level, service, commands, Map3D);
            Map = new MapViewModel(level, service, state, commands);


            // Tool 切り替えコマンド
            SetPlaceBlockToolCommand = new RelayCommand(_ => SetPlaceBlockTool());
            SetPlaceObjectToolCommand = new RelayCommand(_ => SetPlaceObjectTool());
            SetDeleteToolCommand = new RelayCommand(_ => SetDeleteTool());
            SetSelectToolCommand = new RelayCommand(_ => SetSelectTool());

            // Undo/Redoコマンド
            UndoCommand = new RelayCommand(_ => Undo(commands));
            RedoCommand = new RelayCommand(_ => Redo(commands));

            // Save/Openコマンド
            SaveCommand = new RelayCommand(_ => Save());
            OpenCommand = new RelayCommand(_ => Open());

            // カメラリセットコマンド
            CameraResetCommand = new RelayCommand(_ => CameraReset());

            // ブロック/オブジェクト表示/非表示
            state.OnHideBlockRequested += block => Map3D.HideBlock(block);
            state.OnHideObjectRequested += obj => Map3D.HideObject(obj);
            state.OnShowBlockRequested += block => Map3D.ShowBlock(block);
            state.OnShowObjectRequested += obj => Map3D.ShowObject(obj);
        }

        // ブロック配置モード
        private void SetPlaceBlockTool()
        {
            CurrentTool = new PlaceBlockTool(
                Map.State,
                Map.Level,
                Map.Service,
                Map.Commands,
                Map3D
            );

            CurrentTool.OnActivated();
            Map3D.UpdateGhost(Map.State.Ghost);
            Map3D.UpdateOutline(Map.State.Outline);
        }

        // オブジェクト配置モード
        private void SetPlaceObjectTool()
        {
            CurrentTool = new PlaceObjectTool(
                Map.State,
                Map.Level,
                Map.Service,
                Map.Commands,
                Map3D
            );

            CurrentTool.OnActivated();
            Map3D.UpdateGhost(Map.State.Ghost);
            Map3D.UpdateOutline(Map.State.Outline);
        }

        // ブロック/オブジェクト削除モード
        private void SetDeleteTool()
        {
            CurrentTool = new DeleteTool(
                Map.State,
                Map.Level,
                Map.Service,
                Map.Commands,
                Map3D
            );

            CurrentTool.OnActivated();
            Map3D.UpdateGhost(Map.State.Ghost);
            Map3D.UpdateOutline(Map.State.Outline);
        }

        // セレクトモード
        private void SetSelectTool()
        {
            CurrentTool = new SelectTool(
                Map.State,
                Map.Level,
                Map.Service,
                Map.Commands,
                Map3D
            );

            CurrentTool.OnActivated();
            Map3D.UpdateGhost(Map.State.Ghost);
            Map3D.UpdateOutline(Map.State.Outline);
        }
        
        // 3DViewport左クリック
        public void On3DLeftClick(Viewport3D viewport, Point pos)
        {
            var hit = Raycaster.Hit(viewport, pos, Map3D);
            CurrentTool?.OnLeftClick(viewport, pos, hit, Map3D);
        }

        // 3DViewportマウス移動
        public void On3DMouseMove(Viewport3D viewport, Point pos)
        {
            var hit = Raycaster.Hit(viewport, pos, Map3D);
            CurrentTool?.OnMouseMove(viewport, pos, hit, Map3D);
        }

        // 3DViewport左クリック解除
        public void On3DLeftRelease(Viewport3D viewport, Point pos)
        {
            var hit = Raycaster.Hit(viewport, pos, Map3D);
            CurrentTool?.OnLeftRelease(viewport, pos, hit, Map3D);
        }

        private void Undo(CommandManager commands)
        {
            // 選択解除
            ClearSelectionAndOutline();
            commands.Undo();
        }

        private void Redo(CommandManager commands)
        {
            // 選択解除
            ClearSelectionAndOutline();
            commands.Redo();
        }

        // 選択Outline解除
        private void ClearSelectionAndOutline()
        {
            if (Map.State.SelectedBlock != null)
            {
                Map.State.SelectedBlock = null;
                Map.State.Outline.BlockOutline = null;
            }

            if (Map.State.SelectedObject != null)
                Map.State.SelectedObject = null;
                Map.State.Outline.ObjectOutline = null;

            Map3D.UpdateOutline(Map.State.Outline);
        }

        // LevelDataのセーブ
        private void Save()
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Title = "LevelDataを保存",
                Filter = "JSON ファイル (*.json)|*.json",
                DefaultExt = "json",
                AddExtension = true,
                FileName = "level.json"
            };

            if (dialog.ShowDialog() != true)
                return;

            string path = dialog.FileName;

            _saveService.Save(path, Map.Level);

            Debug.WriteLine("Saved: " + path);
        }

        // LevelDataの読み込み
        private void Open()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "LevelDataを開く",
                Filter = "JSON ファイル (*.json)|*.json",
                DefaultExt = "json",
                Multiselect = false
            };

            if (dialog.ShowDialog() != true)
                return;

            string path = dialog.FileName;

            LevelData loaded;
            try
            {
                loaded = _saveService.Load(path);
            }
            catch (Exception ex)
            {
                // JSON の形式が期待と違うなどの読み込みエラーを表示
                MessageBox.Show($"レベルデータの読み込みに失敗しました。", "読み込みエラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (loaded == null)
            {
                MessageBox.Show("読み込まれたデータが無効です。", "読み込みエラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var cmd = new OpenLevelCommand(this, loaded);

            // 選択解除
            ClearSelectionAndOutline();

            // 実行して Undo/Redo 履歴に積む
            Map.Commands.Execute(cmd);

            Debug.WriteLine("Loaded via command: " + path);
        }

        // カメラ位置リセット
        private void CameraReset()
        {
            Map3D.Camera.InitilizeCamera();
        }
    }
}
