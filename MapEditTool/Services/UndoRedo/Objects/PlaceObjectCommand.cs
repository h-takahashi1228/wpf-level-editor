using MapEditTool.Models.Map.ObjectData;

public class PlaceObjectCommand : IEditorCommand
{
    private readonly LevelData _level;
    private readonly ObjectData _obj;

    public PlaceObjectCommand(LevelData level, ObjectData obj)
    {
        _level = level;
        _obj = obj;
    }

    public void Execute()
    {
        _level.AddObject(_obj);
    }

    public void Undo()
    {
        _level.RemoveObject(_obj.Position);
    }
}