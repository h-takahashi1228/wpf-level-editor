namespace MapEditTool.Scene3D
{
    public class GroundScene
    {
        /// 床の幅（X方向）
        public int Width { get; set; } = 100;

        /// 床の奥行き（Z方向）
        public int Depth { get; set; } = 100;

        /// グリッド線の間隔（1なら1mごと）
        public int GridStep { get; set; } = 1;

        /// 床の高さ（通常は0）
        public double GroundY { get; set; } = 0.0;

        /// グリッド線の太さ（論理値）
        public double GridLineThickness { get; set; } = 0.02;

        /// メイン線（10mごと）の太さ
        public double MajorGridLineThickness { get; set; } = 0.05;

        /// メイン線の間隔（10mごと）
        public int MajorGridStep { get; set; } = 10;
    }
}
