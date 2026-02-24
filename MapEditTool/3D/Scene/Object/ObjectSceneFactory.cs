using MapEditTool.Models.Map.ObjectData;

namespace MapEditTool.Scene3D
{
    public static class ObjectSceneFactory
    {
        public static ObjectSceneBase Create(ObjectData obj, bool isGhost = false, bool canPlace = true)
        {
            return obj switch
            {
                PlayerSpawnerData => new PlayerSpawnerScene(obj.Position, obj.Yaw, isGhost, canPlace),
                EnemySpawnerData => new EnemySpawnerScene(obj.Position, obj.Yaw, isGhost, canPlace),
                DefenseBaseData => new DefenseBaseScene(obj.Position, obj.Yaw, isGhost, canPlace),
                _ => throw new NotImplementedException()
            };
        }
    }
}