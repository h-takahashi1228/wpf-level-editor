using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;

public class PlaceBlockCommand : IEditorCommand
{
    private readonly LevelData _level;
    private readonly BlockData _block;

    public PlaceBlockCommand(LevelData level, BlockData block)
    {
        _level = level;
        _block = block;
    }

    public void Execute()
    {
        _level.AddBlock(_block);
    }

    public void Undo()
    {
        _level.RemoveBlock(_block.Position);
    }
}