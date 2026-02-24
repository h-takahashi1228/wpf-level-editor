using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;

public class DeleteBlockCommand : IEditorCommand
{
    private readonly LevelData _level;
    private readonly BlockData _block;

    public DeleteBlockCommand(LevelData level, BlockData block)
    {
        _level = level;
        _block = block;
    }

    public void Execute()
    {
        _level.RemoveBlock(_block.Position);
    }

    public void Undo()
    {
        _level.AddBlock(_block);
    }
}
