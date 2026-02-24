using System.Collections.ObjectModel;

namespace MapEditTool.Models.Map.ObjectData
{
    public class LevelData
    {
        public ObservableCollection<BlockData> Blocks { get; set; } = new();
        public ObservableCollection<ObjectData> Objects { get; set; } = new();

        public readonly Dictionary<GridPos, BlockData> BlockMap = new();
        public readonly Dictionary<GridPos, ObjectData> ObjectMap = new();

        // GridPosにブロックが存在するかチェック
        public bool TryGetBlock(GridPos pos, out BlockData block)
            => BlockMap.TryGetValue(pos, out block);

        // GridPosにオブジェクトが存在するかチェック
        public bool TryGetObject(GridPos pos, out ObjectData obj)
            => ObjectMap.TryGetValue(pos, out obj);

        // GridPosにデータが存在するかチェック
        public bool IsOccupied(GridPos pos)
            => BlockMap.ContainsKey(pos) || ObjectMap.ContainsKey(pos);

        // ブロックの追加
        public void AddBlock(BlockData block)
        {
            Blocks.Add(block);
            BlockMap[block.Position] = block;
        }

        // ブロックの削除
        public void RemoveBlock(GridPos pos)
        {
            if (BlockMap.TryGetValue(pos, out var block))
            {
                Blocks.Remove(block);
                BlockMap.Remove(pos);
            }
        }

        // オブジェクトの追加
        public void AddObject(ObjectData obj)
        {
            Objects.Add(obj);
            ObjectMap[obj.Position] = obj;
        }

        // オブジェクトの削除
        public void RemoveObject(GridPos pos)
        {
            if (ObjectMap.TryGetValue(pos, out var obj))
            {
                Objects.Remove(obj);
                ObjectMap.Remove(pos);
            }
        }

        // ブロック/オブジェクトの再配置
        public void ReplaceWithInstances(IEnumerable<BlockData> blocks, IEnumerable<ObjectData> objects)
        {
            Blocks.Clear();
            Objects.Clear();
            BlockMap.Clear();
            ObjectMap.Clear();

            foreach (var b in blocks)
            {
                Blocks.Add(b);
                BlockMap[b.Position] = b;
            }

            foreach (var o in objects)
            {
                Objects.Add(o);
                ObjectMap[o.Position] = o;
            }
        }
    }

}
