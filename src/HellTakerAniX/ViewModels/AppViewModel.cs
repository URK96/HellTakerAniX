namespace HellTakerAniX.ViewModels;

public partial class AppViewModel : ObservableObject
{
    [RelayCommand]
    private void ExitApp()
    {
        Environment.Exit(0);
    }
}