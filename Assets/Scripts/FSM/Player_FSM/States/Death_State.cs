using FSM;
using UnityEngine;

public class Death_State : State
{
    private Character _character;
    public Death_State(Character character)
    {
        _character = character;
    }

    public override void Enter()
    {
        Debug.Log("Death");
        _character.Components.animator.SetBool("isDead", true);
        DestroyHelper.Destroy(_character.Components.characterTransform.gameObject);
    }
}
