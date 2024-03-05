using FSM;
using UnityEngine;

public class Slowed_State : State
{
    private Character _character;
    private float slowTime = 5f;

    public Slowed_State(Character character)
    {
        _character = character;
    }

    public override void Enter()
    {
        _character.Speed /= 2f;
        Debug.Log("Slowed");
    }

    public override void Update()
    {
        slowTime -= Time.deltaTime;
        if (slowTime <= 0)
        {
            _character.SM.ChancgeState(CharacterStateType.Move);
        }
    }

    public override void Exit()
    {
        _character.Speed *= 2f;
    }
}