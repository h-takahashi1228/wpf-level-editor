using MapEditTool.Models.Map.ObjectData;

namespace MapEditTool.Services.UndoRedo.Objects
{
    class ChangeEnemySpawnerDataCommand : IEditorCommand
    {
        private readonly LevelEditorService _service;
        private readonly EnemySpawnerData _obj;
        private readonly List<EnemySpawnEntry> _oldEnemySpawnData;
        private readonly List<EnemySpawnEntry> _newEnemySpawnData;

        public ChangeEnemySpawnerDataCommand(LevelEditorService service, EnemySpawnerData obj, List<EnemySpawnEntry> oldEnemySpawnData, List<EnemySpawnEntry> newEnemySpawnData)
        {
            _service = service;
            _obj = obj;
            _oldEnemySpawnData = oldEnemySpawnData;
            _newEnemySpawnData = newEnemySpawnData;
        }

        public void Execute()
        {
            _service.ChangeEnemySpawnData(_obj, _newEnemySpawnData);
        }

        public void Undo()
        {
            _service.ChangeEnemySpawnData(_obj, _oldEnemySpawnData);
        }
    }
}
