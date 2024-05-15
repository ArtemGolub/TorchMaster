using FSM;
using UnityEngine;

public class Idle_State : State
{
    private Character _character;
    private float _followDuration = 3;
    private float _elapsedTime = 0;
    
    public Idle_State(Character character)
    {
        _character = character;
    }

    public override void Enter()
    {
        if (_character.Components.animator == null) return;
        _character.Components.animator.SetBool("isIdle", true);

        if (_character.CharacterType == CharacterType.Enemy)
        {
            _elapsedTime = 0f;
        }
    }

    public override void Update()
    {
        if (_character.CharacterType == CharacterType.Enemy)
        {
            if (_elapsedTime >= _followDuration)
            {
                _character.SM.ChancgeState(CharacterStateType.Move);
            }
            _elapsedTime += Time.deltaTime;
        }
    }

    public override void Exit()
    {
        if( _character.Components.animator == null) return;
        _character.Components.animator.SetBool("isIdle", false);
    }
}
