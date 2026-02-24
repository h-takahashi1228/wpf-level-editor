using MapEditTool.Models.Map.ObjectData;
using MapEditTool.Services;

public class ChangeObjectYawCommand : IEditorCommand
{
    private readonly LevelEditorService _service;
    private readonly ObjectData _obj;
    private readonly int _oldYaw;
    private readonly int _newYaw;

    public ChangeObjectYawCommand(LevelEditorService service, ObjectData obj, int oldYaw, int newYaw)
    {
        _service = service;
        _obj = obj;
        _oldYaw = oldYaw;
        _newYaw = newYaw;
    }

    public void Execute()
    {
        _service.ChangeYawObject(_obj, _newYaw);
    }

    public void Undo()
    {
        _service.ChangeYawObject(_obj, _oldYaw);
    }
}
