using UnityEngine;

public class TorchObserver
{
    private IInventory _inventory;
    
    public bool IsTorchBurning()
    {        
        bool anyActive = false;
        if (!_inventory._items.ContainsKey(ItemType.Torch)) return false;
        foreach (var kvp in _inventory._items)
        {
            if (kvp.Key == ItemType.Torch)
            {
                foreach (Item item in kvp.Value)
                {
                    if(item == null) return false;
                    if (item.FSM.CheckState(ItemStateType.Active))
                    {
                        anyActive = true;
                    }
                }
            }
        }
        return anyActive;
    }
    
    public TorchObserver(IInventory inventory)
    {
        _inventory = inventory;
    }
    
    public void CheckBurningObjects(Character character)
    {
        CheckBurnedObjects(character);
        bool anyActive = false;
        if (!_inventory._items.ContainsKey(ItemType.Torch)) return;
        foreach (var kvp in _inventory._items)
        {
            if (kvp.Key == ItemType.Torch)
            {
                foreach (Item item in kvp.Value)
                {
                    if(item == null) return;
                    if (item.FSM.CheckState(ItemStateType.Active))
                    {
                        anyActive = true;
                        character.MadnessCommandManager.SubscribeCommand(CharacterCommandType.EncreaseMadness);
                        character.MadnessCommandManager.UnSubscribeCommand(CharacterCommandType.ReduceMadness);
                        character.Components.animator.SetBool("isTorch", true);
                        break;
                    }
                }
            }
        }
        if (!anyActive)
        {
            ActivateTorch(character);
        }
    }
    
    private void CheckBurnedObjects(Character character)
    {
        if (!_inventory._items.ContainsKey(ItemType.Torch))
        {
            character.Components.animator.SetLayerWeight(1, 0f);
            return;
        }
        foreach (var kvp in _inventory._items)
        {
            if (kvp.Key == ItemType.Torch)
            {
                foreach (Item item in kvp.Value)
                {
                    if(item == null) return;
                    if (item.FSM.CheckState(ItemStateType.Used))
                    {
                        _inventory.RemoveItem(item);
                        character.Components.animator.SetBool("isTorch", false);
                        character.MadnessCommandManager.SubscribeCommand(CharacterCommandType.ReduceMadness);
                        character.MadnessCommandManager.UnSubscribeCommand(CharacterCommandType.EncreaseMadness);
                        character.Components.animator.SetLayerWeight(1, 0f);
                        return;
                    }
                }
            }
        }
    }
    
    private void ActivateTorch(Character character)
    {
        if (_inventory._items.ContainsKey(ItemType.Torch))
        {
            foreach (var kvp in _inventory._items)
            {
                if (kvp.Key == ItemType.Torch)
                {
                    foreach (Item item in kvp.Value)
                    {
                        if (!item.FSM.CheckState(ItemStateType.Active))
                        {
                            character.MadnessCommandManager.SubscribeCommand(CharacterCommandType.EncreaseMadness);
                            character.MadnessCommandManager.UnSubscribeCommand(CharacterCommandType.ReduceMadness);
                            item.FSM.ChangeState(ItemStateType.Active);
                            character.Components.animator.SetLayerWeight(1, 1f);
                            return;
                        }
                    }
                }
            }
        }
        else
        {
            character.MadnessCommandManager.SubscribeCommand(CharacterCommandType.ReduceMadness);
            character.MadnessCommandManager.UnSubscribeCommand(CharacterCommandType.EncreaseMadness);
            character.Components.animator.SetLayerWeight(1, 0f);
        }
    }
}