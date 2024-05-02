using FSM;

public class Idle_State : State
{
    private Character _character;
    public Idle_State(Character character)
    {
        _character = character;
    }
    public override void Enter()
    {
        if( _character.Components.animator == null) return;
        _character.Components.animator.SetBool("isIdle", true);
    }
    public override void Exit()
    {
        if( _character.Components.animator == null) return;
        _character.Components.animator.SetBool("isIdle", false);
    }
}
