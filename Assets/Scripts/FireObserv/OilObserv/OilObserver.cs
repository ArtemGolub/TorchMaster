public class OilObserver
{
    private Character _character;
    private IInventory _inventory;

    public OilObserver(Character character)
    {
        _character = character;
        _inventory = _character.Inventory;
    }

    public void CheckAvaliableOil()
    {
        bool anyActive = false;
        if (!_inventory._items.ContainsKey(ItemType.Oil)) return;
        foreach (var kvp in _inventory._items)
        {
            if (kvp.Key == ItemType.Oil)
            {
                foreach (Item item in kvp.Value)
                {
                    if (item == null) return;
                    if (item.FSM.CheckState(ItemStateType.Active))
                    {
                        anyActive = true;
                        break;
                    }
                }
            }
        }

        if (!anyActive)
        {
            ActivateOil();
        }
    }

    public Item GetActiveOil()
    {
        if (_inventory._items.ContainsKey(ItemType.Oil))
        {
            foreach (var kvp in _inventory._items)
            {
                if (kvp.Key == ItemType.Oil)
                {
                    foreach (Item item in kvp.Value)
                    {
                        item.FSM.ChangeState(ItemStateType.Used);
                        return item;
                    }
                }
            }
        }

        return null;
    }

    private void ActivateOil()
    {
        if (_inventory._items.ContainsKey(ItemType.Oil))
        {
            foreach (var kvp in _inventory._items)
            {
                if (kvp.Key == ItemType.Oil)
                {
                    foreach (Item item in kvp.Value)
                    {
                        if (!item.FSM.CheckState(ItemStateType.Active))
                        {
                            item.FSM.ChangeState(ItemStateType.Active);
                            return;
                        }
                    }
                }
            }
        }
    }
}