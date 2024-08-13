using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace HellTakerAniX.Views
{
    public partial class AnimationWindow : Window
    {
        private bool _isPointerPressed = false;
        private PointerPoint _pressedPointerPoint;

        public AnimationWindow()
        {
            InitializeComponent();

            Width = 100;
            Height = 100;

            DataContext = new AnimationWindowWindowViewModel();
        }

        #region EventHandlers

        private void Window_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsRightButtonPressed)
            {
                return;
            }

            if (WindowState is WindowState.Maximized or
                               WindowState.FullScreen)
            {
                return;
            }

            _isPointerPressed = true;
            _pressedPointerPoint = e.GetCurrentPoint(this);
        }

        private void Window_PointerReleased(object sender, PointerReleasedEventArgs e)
        {
            _isPointerPressed = false;
        }

        private void Window_PointerMoved(object sender, PointerEventArgs e)
        {
            if (_isPointerPressed)
            {
                PointerPoint currentPoint = e.GetCurrentPoint(this);

                Position = new(Position.X + (int)(currentPoint.Position.X - _pressedPointerPoint.Position.X),
                               Position.Y + (int)(currentPoint.Position.Y - _pressedPointerPoint.Position.Y));
            }
        }

        private void RemoveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
    }
}
