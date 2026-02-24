using MapEditTool.Models.Enums;

namespace MapEditTool.Models.Map.ObjectData
{
    public class EnemySpawnEntry
    {
        public EnemyType EnemyType { get; set; } // 敵の種類
        public float Time { get; set; } // 湧き時間
    }
}