using MapEditTool.Models.Enums;
using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;
using MapEditTool.Services;

namespace MapEditTool.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        public LevelData Level { get; }
        public LevelEditorService Service { get; }
        public EditorState State { get; }
        public CommandManager Commands { get; }

        // ブロックのShapeとTypeの一覧を取得する
        public IEnumerable<BlockType> BlockTypes => Enum.GetValues(typeof(BlockType)).Cast<BlockType>();
        public IEnumerable<BlockShape> BlockShapes => Enum.GetValues(typeof(BlockShape)).Cast<BlockShape>();


        public MapViewModel(LevelData level, LevelEditorService service, EditorState state, CommandManager commands)
        {
            Level = level;
            Service = service;
            State = state;
            Commands = commands;
        }

        public ObjectType? CurrentObjectType
        {
            get => State.CurrentObjectType;
            set
            {
                State.CurrentObjectType = value;
                RaisePropertyChanged();
            }
        }
    }
}
