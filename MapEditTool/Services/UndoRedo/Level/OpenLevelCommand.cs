using MapEditTool.Models.Map.ObjectData;
using MapEditTool.Models.Map;
using MapEditTool.ViewModels;

public class OpenLevelCommand : IEditorCommand
{
    private readonly MainViewModel _main;
    private readonly List<BlockData> _beforeBlocks;
    private readonly List<ObjectData> _beforeObjects;
    private readonly List<BlockData> _afterBlocks;
    private readonly List<ObjectData> _afterObjects;

    public OpenLevelCommand(MainViewModel main, LevelData loaded)
    {
        _main = main;

        _beforeBlocks = new List<BlockData>(_main.Map.Level.Blocks);
        _beforeObjects = new List<ObjectData>(_main.Map.Level.Objects);

        _afterBlocks = new List<BlockData>(loaded.Blocks);
        _afterObjects = new List<ObjectData>(loaded.Objects);
    }

    public void Execute()
    {
        _main.Map.Level.ReplaceWithInstances(_afterBlocks, _afterObjects);
        _main.Map3D.ReloadAll(_main.Map.Level);
    }

    public void Undo()
    {
        _main.Map.Level.ReplaceWithInstances(_beforeBlocks, _beforeObjects);
        _main.Map3D.ReloadAll(_main.Map.Level);
    }
}
