using MapEditTool.Models.Map.ObjectData;
using MapEditTool.Services;

public class ChangeDefenseBaseHpCommand : IEditorCommand
{
    private readonly LevelEditorService _service;
    private readonly DefenseBaseData _obj;
    private readonly int _oldHp;
    private readonly int _newHp;

    public ChangeDefenseBaseHpCommand(LevelEditorService service, DefenseBaseData obj, int oldHp, int newHp)
    {
        _service = service;
        _obj = obj;
        _oldHp = oldHp;
        _newHp = newHp;
    }

    public void Execute()
    {
        _service.ChangeDefenseBaseHp(_obj, _newHp);
    }

    public void Undo()
    {
        _service.ChangeDefenseBaseHp(_obj, _oldHp);
    }
}
