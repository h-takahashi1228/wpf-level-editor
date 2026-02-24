using MapEditTool.Models.Map.ObjectData;
using MapEditTool.Services;
using MapEditTool.ViewModel3D;

namespace MapEditTool.ViewModels
{
    public class DefenseBaseObjectViewModel
        : ObjectViewModelBase<DefenseBaseData>
    {
        public DefenseBaseObjectViewModel(
            DefenseBaseData model,
            LevelData level,
            LevelEditorService service,
            EditorState state,
            CommandManager commandManager,
            Map3DViewModel map3D)
            : base(model, level, service, state, commandManager, map3D)
        {
        }

        // HP
        public int HP
        {
            get => Model.HP;
            set
            {
                if (Model.HP == value) return;

                var old = Model.HP;

                _commandManager.Execute(
                    new ChangeDefenseBaseHpCommand(_service, Model, old, value)
                );

                RaisePropertyChanged();
            }
        }

        // UI更新
        public override void Refresh()
        {
            base.Refresh();
            RaisePropertyChanged(nameof(HP));
        }
    }
}