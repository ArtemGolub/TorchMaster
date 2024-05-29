using FSM;
using UnityEngine;

public class TrueSight_UsedState : State
{
    private Item _item;
    private float timer= 1.5f;

    public TrueSight_UsedState(Item item)
    {
        _item = item;
    }

    public override void Enter()
    {
        if (_item.Transform == null) return;
        AudioManager.current.PlaySFX(SoundType.TrueSightCollect);
        _item.Transform.GetChild(1).transform.gameObject.SetActive(false);
        GameObject.FindObjectOfType<Player>().Character.MadnessCommandManager.ExecuteCommand(CharacterCommandType.EncreaseMadnessValue, _item.TrueSightRestore);
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