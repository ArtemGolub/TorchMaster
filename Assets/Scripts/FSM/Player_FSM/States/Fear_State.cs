using FSM;
using UnityEngine;

public class Fear_State : State
{
    private Character _character;
    private float fearTime = 3f;
    public Fear_State(Character character)
    {
        _character = character;
    }

    public override void Enter()
    {
        _character.Components.animator.SetBool("isFeared", true);
        
        var itemToRemove = _character.InventoryCommandManager.GetItem(ItemType.Torch);
        if (itemToRemove != null)
        {
            itemToRemove.FSM.ChangeState(ItemStateType.Used);
            _character.InventoryCommandManager.ExecuteCommand(CharacterCommandType.Throw, itemToRemove);
        }
        
        _character.Components.animator.SetBool("isTorch", false);
        if (_character.Components.animator.GetLayerWeight(1) == 0)
        {
            _character.Components.animator.SetLayerWeight(1, 0f);
        }
        
        AudioManager.current.PlayPlayerSpeak(SoundType.PlayerScream);
        
        _character.CommandManager.UnSubscribeCommand(CharacterCommandType.Move);
        _character.CommandManager.UnSubscribeCommand(CharacterCommandType.Attack);
    }

    public override void Update()
    {
        fearTime -= Time.deltaTime;
        if (fearTime <= 0)
        {
            _character.SM.ChancgeState(CharacterStateType.Idle);
        }
    }

    public override void Exit()
    {
        _character.CommandManager.SubscribeCommand(CharacterCommandType.Attack);
        fearTime = 3f;
        _character.Components.animator.SetBool("isFeared", false);
    }
}