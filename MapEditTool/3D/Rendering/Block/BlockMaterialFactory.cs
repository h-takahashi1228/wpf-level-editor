using System.Windows.Media;
using System.Windows.Media.Media3D;
using MapEditTool.Models.Enums;

namespace MapEditTool.Rendering3D
{
    public static class BlockMaterialFactory
    {
        public static Material Create(BlockType type, bool isGhost)
        {
            Color color = type switch
            {
                BlockType.Grass => Colors.Green,
                BlockType.Sand => Colors.SandyBrown,
                _ => Colors.Gray
            };

            if (isGhost)
            {
                color = Color.FromArgb(120, color.R, color.G, color.B);
            }

            return new DiffuseMaterial(new SolidColorBrush(color));
        }
    }
}
