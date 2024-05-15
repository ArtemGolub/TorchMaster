using FSM;
using UnityEngine;

public class GhostDeath_State : State
{
    private Character _character;
    
    private float curTimer;
    private float maxTimer = 1.5f;
    public GhostDeath_State(Character character)
    {
        _character = character;
    }

    public override void Enter()
    {
        if (_character.Components.animator != null)
        {
            _character.Components.animator.SetBool("isDead", true);
        }

        _character.Components.characterTransform.GetComponent<Collider>().enabled = false;
        _character.CommandManager.UnSubscribeCommand(CharacterCommandType.Move);
        _character.CommandManager.UnSubscribeCommand(CharacterCommandType.Follow);
        curTimer = maxTimer;
    }

    public override void Update()
    {
        if (_character.Components.characterTransform != null)
        {
            float liftAmount = 2 * Time.deltaTime;
            float turnAmount = 180 * Time.deltaTime;
            _character.Components.characterTransform.Translate(Vector3.up * liftAmount);
            _character.Components.characterTransform.Rotate(Vector3.up, turnAmount);

        }
        curTimer -= Time.deltaTime;
        if (curTimer <= 0)
        {
            DestroyHelper.Destroy(_character.Components.characterTransform.gameObject);
        }
    }
}