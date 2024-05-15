using FSM;

public class PlayerUse_State : State
{
    private Character _character;
    
    
    public PlayerUse_State(Character character)
    {
        _character = character;
    }
    
    public override void Enter()
    {
        _character.CommandManager.UnSubscribeCommand(CharacterCommandType.Move);
        _character.Components.animator.SetLayerWeight(1, 100);
        _character.Components.animator.SetBool("isUse", true);
    }
    
    public override void Exit()
    {
        _character.Components.animator.SetLayerWeight(1, 0);
        _character.Components.animator.SetBool("isUse", false);
    }
}