using Unity.VisualScripting;
using UnityEngine;
using State = FSM.State;

public class KeyGrab_State : State
{
    private Item _item;
    private float timer= 1.5f;

    public KeyGrab_State(Item item)
    {
        _item = item;
    }

    public override void Enter()
    {
        if (_item.Transform == null) return;
        // TODO Refactor
        _item.Transform.GetChild(1).transform.gameObject.SetActive(false);
        var Character =  GameObject.FindObjectOfType<Player>().Character;
        Character.CommandManager.ExecuteCommand(CharacterCommandType.Use, Character);
        _item.collectSound.Play();
        
    }

    public override void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            DestroyHelper.Destroy(_item.Transform.gameObject);
        }
    }
}