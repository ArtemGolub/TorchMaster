public class KeyCollectCommand : ICharacterCommand
{
    public void Execute(Character character)
    {
        KeyCanvas.current.Enable(true);
        character.hasKey = true;
    }
}