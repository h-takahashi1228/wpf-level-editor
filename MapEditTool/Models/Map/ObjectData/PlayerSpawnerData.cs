using MapEditTool.Services;

namespace MapEditTool.Models.Map.ObjectData
{
    public class PlayerSpawnerData : ObjectData
    {
        public override object GetViewModel(EditorState state)
            => state.PlayerSpawnerObjectVM;
        public override ObjectData Clone()
        {
            return new PlayerSpawnerData
            {
                Position = this.Position,
                Yaw = this.Yaw
            };
        }
    }

}
