namespace HellTakerAniX.ViewModels;

public partial class AnimationWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private object _contentView;

    public AnimationWindowViewModel()
    {
        _contentView = new CharacterAniView();
    }
}