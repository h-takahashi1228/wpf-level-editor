using MapEditTool.Services;


namespace MapEditTool.Models.Map.ObjectData
{
    public abstract class ObjectData
    {
        public GridPos Position { get; set; } // 座標
        public int Yaw { get; set; } // 向き

        public abstract object GetViewModel(EditorState state);
        public abstract ObjectData Clone();
    }


}