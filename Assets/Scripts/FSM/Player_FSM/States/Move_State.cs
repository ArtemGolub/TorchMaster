using FSM;
public class Move_State : State
{
    private CharacterCommandManager _commandManager;

    public Move_State(CharacterCommandManager commandManager)
    {
        _commandManager = commandManager;
    }

    public override void Enter()
    {
        _commandManager.SubscribeCommand(CommandType.Move);
    }

    public override void Exit()
    {
        _commandManager.UnSubscribeCommand(CommandType.Move);
    }
}
