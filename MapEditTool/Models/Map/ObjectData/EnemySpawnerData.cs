using MapEditTool.Services;

namespace MapEditTool.Models.Map.ObjectData
{
    public class EnemySpawnerData : ObjectData
    {
        public List<EnemySpawnEntry> EnemySpawnData { get; set; } = new(); // 湧き情報

        public override object GetViewModel(EditorState state)
            => state.EnemySpawnerObjectVM;
        public override ObjectData Clone()
        {
            return new EnemySpawnerData
            {
                Position = this.Position,
                Yaw = this.Yaw,
                EnemySpawnData = this.EnemySpawnData
            };
        }
    }

}
