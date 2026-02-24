using MapEditTool.Models.Map;
using MapEditTool.Services;

public class MoveBlockCommand : IEditorCommand
{
    private readonly LevelEditorService _service;
    private readonly BlockData _block;
    private readonly GridPos _oldPos;
    private readonly GridPos _newPos;

    public MoveBlockCommand(LevelEditorService service, BlockData block, GridPos oldPos, GridPos newPos)
    {
        _service = service;
        _block = block;
        _oldPos = oldPos;
        _newPos = newPos;
    }

    public void Execute()
    {
        _service.ChangePosBlock(_block, _newPos);
    }

    public void Undo()
    {
        _service.ChangePosBlock(_block, _oldPos);
    }
}
