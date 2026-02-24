using MapEditTool.Models.Enums;

namespace MapEditTool.Rendering3D
{
    public class ObjectOutlineRendererFactory
    {
        private static readonly Dictionary<ObjectType, IObjectOutlineRenderer> _renderers =
            new()
            {
                { ObjectType.PlayerSpawner, new SpawnerOutlineRenderer() },
                { ObjectType.EnemySpawner, new SpawnerOutlineRenderer() },
                { ObjectType.DefenseBase, new DefenseBaseOutlineRenderer() }
            };
        public static IObjectOutlineRenderer GetRenderer(ObjectType type)
        {
            return _renderers[type];
        }
    }
}
