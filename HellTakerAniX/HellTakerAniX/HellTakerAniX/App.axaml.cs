using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace HellTakerAniX
{
    public partial class App : Application
    {
        internal static List<HTCharacter> Characters { get; private set; }

        public override void Initialize()
        {
            InitCharacterList();

            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void InitCharacterList()
        {
            Characters = new()
            {
                new() { CharacterType = HTCharacterTypeEnum.Azazel, SpriteResourceName = "Azazel.png" },
                new() { CharacterType = HTCharacterTypeEnum.Bellzebub, SpriteResourceName = "Bellzebub.png" },
                new() { CharacterType = HTCharacterTypeEnum.Cerberus, SpriteResourceName = "Cerberus.png" },
                new() { CharacterType = HTCharacterTypeEnum.CerberusFull, SpriteResourceName = "Cerberus.png" },
                new() { CharacterType = HTCharacterTypeEnum.GloriousLeft, SpriteResourceName = "Glorious_Left.png" },
                new() { CharacterType = HTCharacterTypeEnum.GloriousRight, SpriteResourceName = "Glorious_Right.png" },
                new() { CharacterType = HTCharacterTypeEnum.Hero, SpriteResourceName = "Hero.png" },
                new() { CharacterType = HTCharacterTypeEnum.HeroCook, SpriteResourceName = "Hero_Cook.png" },
                new() { CharacterType = HTCharacterTypeEnum.Judgement, SpriteResourceName = "Judgement.png" },
                new() { CharacterType = HTCharacterTypeEnum.Justice, SpriteResourceName = "Justice.png" },
                new() { CharacterType = HTCharacterTypeEnum.Lucifer, SpriteResourceName = "Lucifer.png" },
                new() { CharacterType = HTCharacterTypeEnum.LuciferCook, SpriteResourceName = "Lucifer_Cook.png" },
                new() { CharacterType = HTCharacterTypeEnum.Malina, SpriteResourceName = "Malina.png" },
                new() { CharacterType = HTCharacterTypeEnum.Modeus, SpriteResourceName = "Modeus.png" },
                new() { CharacterType = HTCharacterTypeEnum.Pandemonica, SpriteResourceName = "Pandemonica.png" },
                new() { CharacterType = HTCharacterTypeEnum.Skeleton, SpriteResourceName = "Skeleton.png" },
                new() { CharacterType = HTCharacterTypeEnum.Zdrada, SpriteResourceName = "Zdrada.png" }
            };
        }
    }
}
