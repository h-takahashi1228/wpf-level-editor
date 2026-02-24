using MapEditTool.Models.Map;
using MapEditTool.Models.Map.ObjectData;
using MapEditTool.Services;

public class MoveObjectCommand : IEditorCommand
{
    private readonly LevelEditorService _service;
    private readonly ObjectData _object;
    private readonly GridPos _oldPos;
    private readonly GridPos _newPos;

    public MoveObjectCommand(LevelEditorService service, ObjectData obj, GridPos oldPos, GridPos newPos)
    {
        _service = service;
        _object = obj;
        _oldPos = oldPos;
        _newPos = newPos;
    }

    public void Execute()
    {
        _service.ChangePosObject(_object, _newPos);
    }

    public void Undo()
    {
        _service.ChangePosObject(_object, _oldPos);
    }
}
