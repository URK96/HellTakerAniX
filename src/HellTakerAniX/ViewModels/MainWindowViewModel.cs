namespace HellTakerAniX.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private object _contentView;

    public MainWindowViewModel()
    {
        _contentView = new CharacterAniView();
    }
}