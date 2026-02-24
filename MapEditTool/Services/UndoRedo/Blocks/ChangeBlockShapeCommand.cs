using MapEditTool.Models.Enums;
using MapEditTool.Models.Map;
using MapEditTool.Services;

public class ChangeBlockShapeCommand : IEditorCommand
{
    private readonly LevelEditorService _service;
    private readonly BlockData _block;
    private readonly BlockShape _oldShape;
    private readonly BlockShape _newShape;

    public ChangeBlockShapeCommand(LevelEditorService service, BlockData block, BlockShape oldShape, BlockShape newShape)
    {
        _service = service;
        _block = block;
        _oldShape = oldShape;
        _newShape = newShape;
    }

    public void Execute()
    {
        _service.ChangeShapeBlock(_block, _newShape);
    }

    public void Undo()
    {
        _service.ChangeShapeBlock(_block, _oldShape);
    }
}
