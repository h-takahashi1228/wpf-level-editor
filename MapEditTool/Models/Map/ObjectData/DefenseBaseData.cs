using MapEditTool.Services;

namespace MapEditTool.Models.Map.ObjectData
{
    public class DefenseBaseData : ObjectData
    {
        public int HP { get; set; } // HP
        public override object GetViewModel(EditorState state)
            => state.DefenseBaseObjectVM;
        public override ObjectData Clone()
        {
            return new DefenseBaseData
            {
                Position = this.Position,
                Yaw = this.Yaw,
                HP = this.HP
            };
        }
    }
}
