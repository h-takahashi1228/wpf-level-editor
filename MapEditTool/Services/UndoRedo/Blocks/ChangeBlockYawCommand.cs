using MapEditTool.Models.Map;
using MapEditTool.Services;

public class ChangeBlockYawCommand : IEditorCommand
{
    private readonly LevelEditorService _service;
    private readonly BlockData _block;
    private readonly int _oldYaw;
    private readonly int _newYaw;

    public ChangeBlockYawCommand(LevelEditorService service, BlockData block, int oldYaw, int newYaw)
    {
        _service = service;
        _block = block;
        _oldYaw = oldYaw;
        _newYaw = newYaw;
    }

    public void Execute()
    {
        _service.ChangeYawBlock(_block, _newYaw);
    }

    public void Undo()
    {
        _service.ChangeYawBlock(_block, _oldYaw);
    }
}
