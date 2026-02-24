using System.Windows.Media;
using System.Windows.Media.Media3D;
using MapEditTool.Models.Map.ObjectData;

namespace MapEditTool.Rendering3D
{
    public static class ObjectMaterialFactory
    {
        public static Material Create(object obj, bool isGhost, bool canPlace)
        {
            Color color = obj switch
            {
                PlayerSpawnerData => Colors.Blue,
                EnemySpawnerData => Colors.Yellow,
                _ => Colors.Gray
            };

            if (!canPlace)
                color = Colors.Red;

            if (isGhost)
            {
                color = Color.FromArgb(120, color.R, color.G, color.B);
            }

            return new DiffuseMaterial(new SolidColorBrush(color));
        }
    }
}
