using MapEditTool.Models.Enums;
using MapEditTool.Models.Map;
using MapEditTool.Services;

public class ChangeBlockTypeCommand : IEditorCommand
{
    private readonly LevelEditorService _service;
    private readonly BlockData _block;
    private readonly BlockType _oldType;
    private readonly BlockType _newType;

    public ChangeBlockTypeCommand(LevelEditorService service, BlockData block, BlockType oldType, BlockType newType)
    {
        _service = service;
        _block = block;
        _oldType = oldType;
        _newType = newType;
    }

    public void Execute()
    {
        _service.ChangeTypeBlock(_block, _newType);
    }

    public void Undo()
    {
        _service.ChangeTypeBlock(_block, _oldType);
    }
}
