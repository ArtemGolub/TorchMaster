using FSM;
using UnityEngine;

public class AttackState : State
{
    private Character _character;

    private float curTimer;
    private float maxTimer = 2;
    
    public AttackState(Character character)
    {
        _character = character;
    }

    public override void Enter()
    {
        _character.CommandManager.UnSubscribeCommand(CharacterCommandType.Move);
        _character.Components.animator.SetBool("isFear", true);
        curTimer = maxTimer;
    }

    public override void Update()
    {
        curTimer -= Time.deltaTime;
        if (curTimer <= 0)
        {
            _character.SM.ChancgeState(CharacterStateType.Move);
        }
    }

    public override void Exit()
    {
        _character.Components.animator.SetBool("isFear", false);
    }
}