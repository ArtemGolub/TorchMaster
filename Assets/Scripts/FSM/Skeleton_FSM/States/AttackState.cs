using FSM;
using UnityEngine;

public class AttackState : State
{
    private Character _character;

    private float curTimer;
    private float maxTimer = 2f;

    private AudioSource _attackAudio;
    
    public AttackState(Character character, AudioSource attackAudio)
    {
        _character = character;
        _attackAudio = attackAudio;
    }

    public override void Enter()
    {
        _character.CommandManager.UnSubscribeCommand(CharacterCommandType.Move);
        _character.Components.animator.SetBool("isFear", true);
        _attackAudio.Play();
        curTimer = maxTimer;
    }

    public override void Update()
    {
        curTimer -= Time.deltaTime;
        if (curTimer <= 0)
        {
            _character.SM.ChancgeState(CharacterStateType.Move);
            if (_character.Name == "GhostFollow")
            {
                DestroyHelper.Destroy(_character.Components.characterTransform.gameObject);
            }
        }
    }

    public override void Exit()
    {
        _character.Components.animator.SetBool("isFear", false);
    }
}