using MapEditTool.Models.Enums;

namespace MapEditTool.Rendering3D
{
    public static class ObjectRendererFactory
    {
        private static readonly Dictionary<ObjectType, IObjectRenderer> _renderers =
            new()
            {
                { ObjectType.PlayerSpawner, new PlayerSpawnerRenderer() },
                { ObjectType.EnemySpawner, new EnemySpawnerRenderer() },
                { ObjectType.DefenseBase, new DefenseBaseRenderer() },
            };

        public static IObjectRenderer GetRenderer(ObjectType type)
        {
            return _renderers[type];
        }
    }

}
