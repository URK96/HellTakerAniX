using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Threading;

namespace HellTakerAniX
{
    public partial class MainWindow : Window
    {
        public DispatcherTimer FrameTimer;

        private readonly List<IImage> _frames = new(12);

        private bool _isPointerPressed = false;
        private PointerPoint _pressedPointerPoint;
        private int _frameCount = 0;

        public MainWindow()
        {
            InitializeComponent();

            Width = 100;
            Height = 100;

            InitCharacterContextMenuItems();

            string resourceFileName = CharacterManager.GetSpriteResourceName(
                SettingManager.Instance.Setting.CharacterType);

            CreateAnimationList(resourceFileName);

            FrameTimer = new()
            {
                Interval = TimeSpan.FromSeconds(1 / 18.0)
            };

            FrameTimer.Tick += FrameTimer_Tick;
            FrameTimer.Start();
        }

        private void InitCharacterContextMenuItems()
        {
            List<RadioButton> list = new();

            foreach (HTCharacter character in CharacterManager.Characters)
            {
                RadioButton button = new()
                {
                    Content = character.CharacterType,
                    GroupName = "CharacterSelectionRadioGroup",
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch
                };

                button.Click += (sender, e) =>
                {
                    ContextFlyout.Hide();

                    SettingManager.Instance.Setting.CharacterType = character.CharacterType;

                    CreateAnimationList(character.SpriteResourceName);
                };

                list.Add(button);
            }

            CharacterContextMenuItem.Items = list;
        }

        private void CreateAnimationList(string fileName)
        {
            FrameTimer?.Stop();

            string bitmapPath = @$"Resources/{fileName}";
            Bitmap bitmap = Bitmap.DecodeToHeight(File.OpenRead(bitmapPath), 100);
            int frame = (int)bitmap.Size.Width / 100;

            foreach (IImage frameImage in _frames)
            {
                (frameImage as CroppedBitmap)?.Dispose();
            }

            _frames.Clear();

            for (int i = 0; i < frame; ++i)
            {
                _frames.Add(new CroppedBitmap(bitmap, new(100 * i, 0, 100, 100)));
            }

            FrameTimer?.Start();
        }

        #region EventHandlers

        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            AnimationBox1.Source = _frames[_frameCount++];

            _frameCount %= _frames.Count;
        }

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
                if (_pressedPointerPoint is null)
                {
                    return;
                }

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
