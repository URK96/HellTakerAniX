namespace HellTakerAniX.ViewModels;

public partial class AnimationWindowWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private object _contentView;

    public AnimationWindowWindowViewModel()
    {
        _contentView = new CharacterAniView();
    }
}