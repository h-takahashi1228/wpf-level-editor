using MapEditTool.Models.Map.ObjectData;

public class DeleteObjectCommand : IEditorCommand
{
    private readonly LevelData _level;
    private readonly ObjectData _obj;

    public DeleteObjectCommand(LevelData level, ObjectData obj)
    {
        _level = level;
        _obj = obj;
    }

    public void Execute()
    {
        _level.RemoveObject(_obj.Position);
    }

    public void Undo()
    {
        _level.AddObject(_obj);
    }
}
