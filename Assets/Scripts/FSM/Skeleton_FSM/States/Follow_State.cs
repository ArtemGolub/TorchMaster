using FSM;
using Movement;
public class Follow_State : State
{
    private CharacterCommandManager _commandManager;
    private Character _character;
    
    public Follow_State(Character character,CharacterCommandManager commandManager)
    {
        _character = character;
        _commandManager = commandManager;
    }

    public override void Enter()
    {
        _commandManager.SubscribeCommand(CharacterCommandType.Follow);
        if( _character.Components.animator == null) return;
        _character.Components.animator.SetBool("isRunning", true);
    }

    public override void Exit()
    {
        _commandManager.UnSubscribeCommand(CharacterCommandType.Follow);
        if( _character.Components.animator == null) return;
        _character.Components.animator.SetBool("isRunning", false);
    }
}
