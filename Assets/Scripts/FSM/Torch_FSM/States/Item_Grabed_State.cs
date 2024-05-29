using FSM;
using UnityEngine;

public class Item_Grabed_State : State
{
    private Item _item;
    public Item_Grabed_State(Item item)
    {
        _item = item;
    }

    public override void Enter()
    {
        if (_item.ItemType == ItemType.Torch)
        {
            AudioManager.current.PlaySFX(SoundType.TorchCollect);
            
            _item.Transform.GetChild(0).transform.gameObject.SetActive(false);
            _item.Transform.GetComponent<Collider>().enabled = false;
            
            foreach (Transform lightPoint in  _item.LightPoint)
            {
                lightPoint.transform.gameObject.SetActive(false);
            }
        }
    }
}