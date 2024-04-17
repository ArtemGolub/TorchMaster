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
        _character.Components.animator.SetBool("isIdle", true);
    }
    public override void Exit()
    {
        _character.Components.animator.SetBool("isIdle", false);
    }
}
