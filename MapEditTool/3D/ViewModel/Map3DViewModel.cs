using MapEditTool.Rendering3D;
using MapEditTool.Scene3D;
using MapEditTool.View3D;
using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;
using MapEditTool.Services;
using MapEditTool.ViewModels;
using System.Collections.Specialized;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MapEditTool.ViewModel3D
{
    public class Map3DViewModel
    {
        // 3D 表示のルート
        public Model3DGroup GeometryRoot { get; } = new Model3DGroup();
        // ライト
        public Model3DGroup LightRoot { get; } = new Model3DGroup();

        // カメラ
        public CameraViewModel Camera { get; } = new CameraViewModel();

        // Visual → GridPos の辞書
        public VisualHitRegistry HitRegistry { get; } = new VisualHitRegistry();

        private readonly LevelData _level;
        // 親ViewModel
        public MainViewModel Parent { get; }
        // ゴースト用モデル
        public Model3D GhostModel;
        // アウトライン用モデル
        public Model3D OutlineModel { get; private set; }

        public Map3DViewModel(LevelData level, MainViewModel parent)
        {
            _level = level;
            Parent = parent;

            _level.Blocks.CollectionChanged += OnBlocksChanged;
            _level.Objects.CollectionChanged += OnObjectsChanged;

            Initialize3D();
        }

        private void Initialize3D()
        {
            LightRoot.Children.Add(
                new DirectionalLight(Colors.White, new Vector3D(0, -1, 0))
            );

            var groundScene = new GroundScene();
            var groundRenderer = new GroundRenderer(groundScene);
            GeometryRoot.Children.Add(groundRenderer.Render());
        }

        // LevelDataから3D Sceneを構築
        public void BuildSceneFromLevel()
        {
            GeometryRoot.Children.Clear();
            HitRegistry.Clear();

            // Ground
            var groundScene = new GroundScene();
            var groundRenderer = new GroundRenderer(groundScene);
            GeometryRoot.Children.Add(groundRenderer.Render());

            // Blocks
            foreach (var block in _level.Blocks)
            {
                AddBlockVisual(block);
            }
        }

        // レベルを丸ごと再読み込みして 3D 表示を更新する
        public void ReloadAll(LevelData level)
        {
            // Clear existing visuals and registry
            GeometryRoot.Children.Clear();
            HitRegistry.Clear();

            // Ground
            var groundScene = new GroundScene();
            var groundRenderer = new GroundRenderer(groundScene);
            GeometryRoot.Children.Add(groundRenderer.Render());

            // Blocks
            foreach (var block in _level.Blocks)
            {
                AddBlockVisual(block);
            }

            // Objects
            foreach (var obj in _level.Objects)
            {
                AddObjectVisual(obj);
            }
        }

        // ブロック関連
        public void AddBlockVisual(BlockData block)
        {
            // Scene を生成
            var scene = BlockSceneFactory.Create(block, isGhost: false);

            // Scene が持つ Model を取得
            var model = scene.Model;

            // HitRegistry に登録
            HitRegistry.BlockRegister(model, block);

            // SceneGraph に追加
            GeometryRoot.Children.Add(model);
        }

        public void DeleteBlockVisual(BlockData block)
        {
            HitRegistry.TryGetBlockModel(block, out Model3D model);

            // VisualHitRegistry から削除
            HitRegistry.BlockUnregister(model, block);

            GeometryRoot.Children.Remove(model);
        }
        private void OnBlocksChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (BlockData block in e.NewItems)
                {
                    AddBlockVisual(block);
                }
            }

            if (e.OldItems != null)
            {
                foreach (BlockData block in e.OldItems)
                {
                    DeleteBlockVisual(block);
                }
            }
        }
        public void ShowBlock(BlockData block)
        {
            if (HitRegistry.TryGetBlockModel(block, out var model))
            {
                GeometryRoot.Children.Add(model);
            }
        }

        public void HideBlock(BlockData block)
        {
            if (HitRegistry.TryGetBlockModel(block, out var model))
            {
                GeometryRoot.Children.Remove(model);
            }
        }
        // オブジェクト関連
        public void AddObjectVisual(ObjectData obj)
        {
            // Scene を生成
            var scene = ObjectSceneFactory.Create(obj, isGhost: false);

            // Scene が持つ Model を取得
            var model = scene.Model;

            // HitRegistry に登録
            HitRegistry.ObjectRegister(model, obj);

            // SceneGraph に追加
            GeometryRoot.Children.Add(model);
        }

        public void DeleteObjectVisual(ObjectData obj)
        {
            HitRegistry.TryGetObjectModel(obj, out Model3D model);

            // VisualHitRegistry から削除
            HitRegistry.ObjectUnregister(model, obj);

            GeometryRoot.Children.Remove(model);
        }
        private void OnObjectsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ObjectData obj in e.NewItems)
                {
                    AddObjectVisual(obj);
                }
            }

            if (e.OldItems != null)
            {
                foreach (ObjectData obj in e.OldItems)
                {
                    DeleteObjectVisual(obj);
                }
            }
        }
        public void ShowObject(ObjectData obj)
        {
            if (HitRegistry.TryGetObjectModel(obj, out var model))
            {
                GeometryRoot.Children.Add(model);
            }
        }
        public void HideObject(ObjectData obj)
        {
            if (HitRegistry.TryGetObjectModel(obj, out var model))
            {
                GeometryRoot.Children.Remove(model);
            }
        }

        public void UpdateGhost(GhostState ghost)
        {
            // 既存のゴースト削除
            if (GhostModel != null)
            {
                GeometryRoot.Children.Remove(GhostModel);
                GhostModel = null;
            }

            if (ghost.BlockGhost != null)
            {
                var block = ghost.BlockGhost;
                var scene = BlockSceneFactory.Create(block, isGhost: true);
                GhostModel = scene.Model;
                GeometryRoot.Children.Add(GhostModel);
            }
            else if (ghost.ObjectGhost != null)
            {
                var obj = ghost.ObjectGhost;

                bool canPlace = true;

                // PlayerSpawner の場合
                if (obj is PlayerSpawnerData)
                {
                    canPlace = Parent.Map.State.CanPlayerSpawnerPlace;
                }
                // DefenseBase の場合
                else if (obj is DefenseBaseData)
                {
                    canPlace = Parent.Map.State.CanDefenseBasePlace;
                }

                var scene = ObjectSceneFactory.Create(obj, isGhost: true, canPlace);
                GhostModel = scene.Model;
                GeometryRoot.Children.Add(GhostModel);
            }

        }
        public void UpdateOutline(OutlineState outline)
        {
            // 既存のアウトライン削除
            if (OutlineModel != null)
            {
                GeometryRoot.Children.Remove(OutlineModel);
                OutlineModel = null;
            }

            if (outline.BlockOutline != null)
            {
                var block = outline.BlockOutline;
                var scene = BlockOutlineSceneFactory.Create(block);
                OutlineModel = scene.Model;
                GeometryRoot.Children.Add(OutlineModel);
            }
            else if (outline.ObjectOutline != null)
            {
                var obj = outline.ObjectOutline;
                var scene = ObjectOutlineSceneFactory.Create(obj);
                OutlineModel = scene.Model;
                GeometryRoot.Children.Add(OutlineModel);
            }
        }
    }
}
