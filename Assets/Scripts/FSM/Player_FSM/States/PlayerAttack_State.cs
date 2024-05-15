using FSM;
using UnityEngine;

public class PlayerAttack_State : State
{
    private Character _character;

    private float curTimer;
    private float maxTimer = 0.6f;
    
    public PlayerAttack_State(Character character)
    {
        _character = character;
    }

    public override void Enter()
    {
        _character.CommandManager.UnSubscribeCommand(CharacterCommandType.Move);
        _character.Components.animator.SetBool("isAttack", true);
        curTimer = maxTimer;
    }

    public override void Update()
    {
        curTimer -= Time.deltaTime;
        if (curTimer <= 0)
        {
            _character.SM.ChancgeState(CharacterStateType.Idle);
        }
        
    }

    public override void Exit()
    {
        curTimer = maxTimer;
        _character.Components.animator.SetBool("isAttack", false);
    }
}