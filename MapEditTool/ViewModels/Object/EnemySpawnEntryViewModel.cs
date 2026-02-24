using MapEditTool.Models.Enums;
using MapEditTool.Models.Map.ObjectData;
using MapEditTool.ViewModels;

public class EnemySpawnEntryViewModel : ViewModelBase
{
    public EnemySpawnEntry Model { get; }

    // 敵の種類
    public EnemyType EnemyType
    {
        get => Model.EnemyType;
        set
        {
            if (Model.EnemyType == value) return;
            Model.EnemyType = value;
            RaisePropertyChanged();
            Edited?.Invoke(this, EventArgs.Empty);
        }
    }

    // 湧き時間
    public float SpawnTime
    {
        get => Model.Time;
        set
        {
            if (Model.Time == value) return;
            Model.Time = value;
            RaisePropertyChanged();
            Edited?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler Edited;

    public EnemySpawnEntryViewModel(EnemySpawnEntry model)
    {
        Model = model;
    }
}
