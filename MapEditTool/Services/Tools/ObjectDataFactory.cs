using MapEditTool.Models.Enums;
using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;

namespace MapEditTool.Services.Tools
{
    public static class ObjectDataFactory
    {
        public static ObjectData Create(ObjectType type, GridPos pos, int yaw)
        {
            // ObjectTypeの種類から作成する型を指定
            return type switch
            {
                ObjectType.DefenseBase => new DefenseBaseData
                {
                    Position = pos,
                    Yaw = yaw
                },

                ObjectType.PlayerSpawner => new PlayerSpawnerData
                {
                    Position = pos,
                    Yaw = yaw
                },

                ObjectType.EnemySpawner => new EnemySpawnerData
                {
                    Position = pos,
                    Yaw = yaw
                },

                _ => throw new NotSupportedException($"Unknown type: {type}")
            };
        }
    }

}
