namespace MapEditTool.Models.Map
{
    public struct GridPos
    {
        // 座標
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public GridPos(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // 座標一致チェック
        public override bool Equals(object obj)
        {
            if (obj is not GridPos other) return false;
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override int GetHashCode()
            => HashCode.Combine(X, Y, Z);

        public static bool operator ==(GridPos a, GridPos b)
            => a.Equals(b);

        public static bool operator !=(GridPos a, GridPos b)
            => !a.Equals(b);

    }
}
