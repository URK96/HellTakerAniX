namespace HellTakerAniX.Models;

internal record HTCharacter
{
    public HTCharacterTypeEnum CharacterType { get; init; }
    public string SpriteResourceName { get; init; }
}
