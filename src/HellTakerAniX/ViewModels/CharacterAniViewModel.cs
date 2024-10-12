using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace HellTakerAniX.ViewModels;

public partial class CharacterAniViewModel : ObservableObject
{
    [ObservableProperty]
    private IImage _currentFrameImage;
    private HTCharacterTypeEnum _currentCharacter = HTCharacterTypeEnum.Azazel;
    private readonly List<IImage> _frames = new(12);
    private readonly FrameTimerService _frameTimerService = App.Services.GetService<FrameTimerService>();
    private bool _isFrameResourceUpdating = false;

    public RadioButton[] CharacterSelectionButtons { get; private set; }

    public CharacterAniViewModel()
    {
        _frameTimerService.NextFrameInvoked += FrameTimerService_NextFrameInvoked;

        InitCharacterContextMenuItems();
        UpdateCharacterResources();
    }

    private void InitCharacterContextMenuItems()
    {
        List<RadioButton> buttons = new(CharacterManager.Characters.Count);

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
                SettingManager.Instance.Setting.CharacterType = character.CharacterType;

                CreateAnimationList(character.SpriteResourceName);
            };

            buttons.Add(button);
        }

        CharacterSelectionButtons = [.. buttons];
    }

    private void UpdateCharacterResources()
    {
        string resourceFileName = CharacterManager.GetSpriteResourceName(_currentCharacter);

        CreateAnimationList(resourceFileName);
    }

    private void CreateAnimationList(string fileName)
    {
        _isFrameResourceUpdating = true;

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

        _isFrameResourceUpdating = false;
    }

    private void FrameTimerService_NextFrameInvoked(object sender, int currentFrameCount)
    {
        if (!_isFrameResourceUpdating)
        {
            UpdateFrameImage(currentFrameCount);
        }
    }

    private void UpdateFrameImage(int currentFrameCount)
    {
        int frameIndex = currentFrameCount % _frames.Count;

        CurrentFrameImage = _frames[frameIndex];
    }
}