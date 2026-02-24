using MapEditTool.Models.Map.ObjectData;
using MapEditTool.Services;
using MapEditTool.ViewModel3D;

namespace MapEditTool.ViewModels
{
    public class PlayerSpawnerObjectViewModel
        : ObjectViewModelBase<PlayerSpawnerData>
    {
        public PlayerSpawnerObjectViewModel(
            PlayerSpawnerData model,
            LevelData level,
            LevelEditorService service,
            EditorState state,
            CommandManager commandManager,
            Map3DViewModel map3D)
            : base(model, level, service, state, commandManager, map3D)
        {
        }

        public override void Refresh()
        {
            base.Refresh();
        }
    }
}