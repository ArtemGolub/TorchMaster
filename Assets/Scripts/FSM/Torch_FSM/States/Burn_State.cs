using FSM;
using UnityEngine;

public class Burn_State : State
{
    private float _lifeTime;
    public Burn_State(float lifeTime)
    {
        _lifeTime = lifeTime;
    }


    public override void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            
        }
    }
}