using System.Timers;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.Input;

namespace HellTakerAniX.ViewModels;

public partial class CharacterAniViewModel : ObservableObject
{
    [ObservableProperty]
    private IImage _currentFrameImage;
    [ObservableProperty]
    private IRelayCommand _removeCharacterCommand;
    private HTCharacterTypeEnum _currentCharacter = HTCharacterTypeEnum.Azazel;
    private readonly System.Timers.Timer _frameTimer;
    private readonly List<IImage> _frames = new(12);
    private int _frameCount = 0;

    public RadioButton[] CharacterSelectionButtons { get; private set; }

    public CharacterAniViewModel()
    {
        _frameTimer = new(TimeSpan.FromSeconds(1 / 18.0));
        
        _frameTimer.Elapsed += FrameTime_Elapsed;

        InitializeCommand();
        InitCharacterContextMenuItems();
        UpdateCharacterResources();
    }

    private void InitializeCommand()
    {
        // _removeCharacterCommand = new RelayCommand()
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
        _frameTimer?.Stop();

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

        UpdateFrameImage();
        _frameTimer?.Start();
    }

    private void FrameTime_Elapsed(object sender, ElapsedEventArgs e)
    {
        UpdateFrameImage();
    }

    private void UpdateFrameImage()
    {
        _frameCount %= _frames.Count;
        CurrentFrameImage = _frames[_frameCount++];        
    }
}