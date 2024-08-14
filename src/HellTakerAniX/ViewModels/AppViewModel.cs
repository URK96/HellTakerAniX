namespace HellTakerAniX.ViewModels;

public partial class AppViewModel : ObservableObject
{
    [RelayCommand]
    private void AddCharacterView()
    {
        CharacterAniWindow window = new();

        window.Show();
    }

    [RelayCommand]
    private void ExitApp()
    {
        Environment.Exit(0);
    }
}