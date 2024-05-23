using System;
using UnityEngine;

public class AnimationEventHandler: MonoBehaviour
{
    public event Action<IFirePoint, Item, Character> OnAnimationEnd;

    private IFirePoint _currentFirePoint;
    private Item _currentTorch;
    private Character _currentCharacter;

    private void Start()
    {
        OnAnimationEnd += BurnFirePoint;
    }
    public void HandleCollision(IFirePoint firePoint, Item torch, Character character)
    {
        _currentFirePoint = firePoint;
        _currentTorch = torch;
        _currentCharacter = character;
    }
    private void BurnFirePoint(IFirePoint firePoint, Item torch, Character _character)
    {
       // _character.InventoryCommandManager.ExecuteCommand(CharacterCommandType.Throw, torch);
       // torch.FSM.ChangeState(ItemStateType.Used);
       BurnObserver.current.ReduceBurn(2);
        firePoint.Burn();
        _character.SM.ChancgeState(CharacterStateType.Idle);
    }
    
    
    public void AnimationEndHandler()
    {
        if (_currentFirePoint == null || _currentTorch == null)
        {
            _currentCharacter.SM.ChancgeState(CharacterStateType.Idle);
            return;
        }
        if (OnAnimationEnd != null)
        {
            OnAnimationEnd.Invoke(_currentFirePoint, _currentTorch, _currentCharacter);
        }
    }
}