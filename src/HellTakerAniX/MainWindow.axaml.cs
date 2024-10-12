using Avalonia.Controls;

namespace HellTakerAniX;

public partial class MainWindow : Window
{
    private readonly List<AnimationWindow> _currentRunningAnimationWindowList = [];

    public MainWindow()
    {
        InitializeComponent();
        CreateNewAnimationWindow();
    }

    private void CreateNewAnimationWindow()
    {
        AnimationWindow window = new();

        window.Show();
        _currentRunningAnimationWindowList.Add(window);
    }
}