using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;
using System.Windows.Media.Media3D;

namespace MapEditTool.View3D
{
    public class VisualHitRegistry
    {
        private readonly Dictionary<Model3D, BlockData> _blockMap = new();
        private readonly Dictionary<BlockData, Model3D> _blockMapReverse = new();
        private readonly Dictionary<Model3D, ObjectData> _objectMap = new();
        private readonly Dictionary<ObjectData, Model3D> _objectMapReverse = new();

        public void BlockRegister(Model3D model, BlockData blockData)
        {
            _blockMap[model] = blockData;
            _blockMapReverse[blockData] = model;
        }

        public bool TryGetBlockData(Model3D model, out BlockData blockData)
            => _blockMap.TryGetValue(model, out blockData);

        public bool TryGetBlockModel(BlockData blockData, out Model3D model)
            => _blockMapReverse.TryGetValue(blockData, out model);

        public void BlockUnregister(Model3D model, BlockData blockData)
        {
            _blockMap.Remove(model);
            _blockMapReverse.Remove(blockData);
        }
        public void ObjectRegister(Model3D model, ObjectData objectData)
        {
            _objectMap[model] = objectData;
            _objectMapReverse[objectData] = model;
        }

        public bool TryGetObjectData(Model3D model, out ObjectData objectData)
            => _objectMap.TryGetValue(model, out objectData);

        public bool TryGetObjectModel(ObjectData objectData, out Model3D model)
            => _objectMapReverse.TryGetValue(objectData, out model);

        public void ObjectUnregister(Model3D model, ObjectData objectData)
        {
            _objectMap.Remove(model);
            _objectMapReverse.Remove(objectData);
        }
        public void Clear()
        {
            _blockMap.Clear();
            _blockMapReverse.Clear();
            _objectMap.Clear();
            _objectMapReverse.Clear();
        }
    }
}
