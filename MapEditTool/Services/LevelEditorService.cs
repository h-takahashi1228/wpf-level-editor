using MapEditTool.Models.Enums;
using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;
using MapEditTool.Services.Tools;

namespace MapEditTool.Services
{
    public class LevelEditorService
    {
        private readonly LevelData _level;

        public LevelEditorService(LevelData level)
        {
            _level = level;
        }

        // ブロックの位置変更
        public void ChangePosBlock(BlockData block, GridPos newPos)
        {
            if (_level.BlockMap.TryGetValue(block.Position, out var existingBlock))
            {
                _level.BlockMap.Remove(existingBlock.Position);

                existingBlock.Position = newPos;

                _level.BlockMap[existingBlock.Position] = existingBlock;

                _level.Blocks.Remove(existingBlock);
                _level.Blocks.Add(existingBlock);
            }
            else if (_level.Blocks.Contains(block))
            {
                _level.BlockMap.Remove(block.Position);
                block.Position = newPos;
                _level.BlockMap[block.Position] = block;
                _level.Blocks.Remove(block);
                _level.Blocks.Add(block);
            }
            else
            {
                var found = _level.Blocks.FirstOrDefault(b => b.Position.Equals(block.Position));
                if (found != null)
                {
                    _level.BlockMap.Remove(found.Position);
                    found.Position = newPos;
                    _level.BlockMap[found.Position] = found;
                    _level.Blocks.Remove(found);
                    _level.Blocks.Add(found);
                }
                else
                {
                    block.Position = newPos;
                    _level.BlockMap[block.Position] = block;
                    _level.Blocks.Add(block);
                }
            }
        }

        // ブロックの形状変更
        public void ChangeShapeBlock(BlockData block, BlockShape newShape)
        {
            block.Shape = newShape;

            _level.BlockMap[block.Position] = block;

            _level.Blocks.Remove(block);
            _level.Blocks.Add(block);
        }
        public void ChangeTypeBlock(BlockData block, BlockType newType)
        {
            block.Type = newType;

            _level.BlockMap[block.Position] = block;

            _level.Blocks.Remove(block);
            _level.Blocks.Add(block);
        }
        public void ChangeYawBlock(BlockData block, int newYaw)
        {
            block.Yaw = newYaw;

            _level.BlockMap[block.Position] = block;

            _level.Blocks.Remove(block);
            _level.Blocks.Add(block);
        }

        // オブジェクトの位置変更
        public void ChangePosObject(ObjectData obj, GridPos newPos)
        {
            if (_level.ObjectMap.TryGetValue(obj.Position, out var existingObj))
            {
                _level.ObjectMap.Remove(existingObj.Position);

                existingObj.Position = newPos;

                _level.ObjectMap[existingObj.Position] = existingObj;

                _level.Objects.Remove(existingObj);
                _level.Objects.Add(existingObj);
            }
            else if (_level.Objects.Contains(obj))
            {
                _level.ObjectMap.Remove(obj.Position);
                obj.Position = newPos;
                _level.ObjectMap[obj.Position] = obj;
                _level.Objects.Remove(obj);
                _level.Objects.Add(obj);
            }
            else
            {
                var found = _level.Objects.FirstOrDefault(o => o.Position.Equals(obj.Position));
                if (found != null)
                {
                    _level.ObjectMap.Remove(found.Position);
                    found.Position = newPos;
                    _level.ObjectMap[found.Position] = found;
                    _level.Objects.Remove(found);
                    _level.Objects.Add(found);
                }
                else
                {
                    obj.Position = newPos;
                    _level.ObjectMap[obj.Position] = obj;
                    _level.Objects.Add(obj);
                }
            }
        }

        // オブジェクトの向き変更
        public void ChangeYawObject(ObjectData obj, int newYaw)
        {
            obj.Yaw = newYaw;

            _level.ObjectMap[obj.Position] = obj;

            _level.Objects.Remove(obj);
            _level.Objects.Add(obj);
        }

        // DefenseBaseのHP変更
        public void ChangeDefenseBaseHp(DefenseBaseData obj, int newHp)
        {
            obj.HP = newHp;

            _level.ObjectMap[obj.Position] = obj;

            _level.Objects.Remove(obj);
            _level.Objects.Add(obj);
        }

        // EnemySpawnerのデータ変更
        public void ChangeEnemySpawnData(EnemySpawnerData obj, List<EnemySpawnEntry> newEnemySpawnData)
        {
            obj.EnemySpawnData = newEnemySpawnData;

            _level.ObjectMap[obj.Position] = obj;

            _level.Objects.Remove(obj);
            _level.Objects.Add(obj);
        }
        
    }
}
