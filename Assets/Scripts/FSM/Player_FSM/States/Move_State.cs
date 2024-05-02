using FSM;
public class Move_State : State
{
    private CharacterCommandManager _commandManager;
    private Character _character;

    public Move_State(Character character,CharacterCommandManager commandManager)
    {
        _character = character;
        _commandManager = commandManager;
    }

    public override void Enter()
    {
        _commandManager.SubscribeCommand(CharacterCommandType.Move);
        if( _character.Components.animator == null) return;
        _character.Components.animator.SetBool("isRunning", true);
    }

    public override void Exit()
    {
        _commandManager.UnSubscribeCommand(CharacterCommandType.Move);
        if( _character.Components.animator == null) return;
        _character.Components.animator.SetBool("isRunning", false);
    }
}
