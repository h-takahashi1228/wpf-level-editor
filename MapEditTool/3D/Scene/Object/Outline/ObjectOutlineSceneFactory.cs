using MapEditTool.Models.Enums;
using MapEditTool.Models.Map.ObjectData;

namespace MapEditTool.Scene3D
{
    public static class ObjectOutlineSceneFactory
    {
        public static ObjectOutlineSceneBase Create(ObjectData obj)
        {
            return obj switch
            {
                PlayerSpawnerData => new SpawnerOutlineScene(obj.Position, obj.Yaw),
                EnemySpawnerData => new SpawnerOutlineScene(obj.Position, obj.Yaw),
                DefenseBaseData => new DefenseBaseOutlineScene(obj.Position, obj.Yaw),
                _ => throw new NotImplementedException()
            };
        }
    }
}
