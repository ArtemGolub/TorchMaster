using FSM;
using UnityEngine;

public class Burn_State : State
{
    private Item _item;
    private float _lifeTime;
    public Burn_State(Item item)
    {
        _item = item;
        _lifeTime = _item.LifeTime;
    }


    public override void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            _item.SM.Burned();
        }
    }
}