using MapEditTool.Models.Enums;

namespace MapEditTool.Helpers
{
    public static class EnumProvider
    {
        public static IEnumerable<BlockShape> BlockShapes =>
            Enum.GetValues(typeof(BlockShape)).Cast<BlockShape>();

        public static IEnumerable<BlockType> BlockTypes =>
            Enum.GetValues(typeof(BlockType)).Cast<BlockType>();

        public static IEnumerable<ObjectType> ObjectTypes =>
            Enum.GetValues(typeof(ObjectType)).Cast<ObjectType>();
        public static IEnumerable<EnemyType> EnemyType =>
            Enum.GetValues(typeof(EnemyType)).Cast<EnemyType>();
    }
}
