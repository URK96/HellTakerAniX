using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace HellTakerAniX.Views;

public partial class CharacterAniView : UserControl
{
    public CharacterAniView()
    {
        InitializeComponent();

        DataContext = new CharacterAniViewModel();
    }
}