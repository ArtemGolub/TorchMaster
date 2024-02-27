using FSM;
using UnityEngine;

public class Burned_State : State
{
    private Transform _transform;
    
    public Burned_State(Transform transform)
    {
        _transform = transform;
    }

    public override void Enter()
    {
        _transform.SetParent(null);
        Debug.Log(_transform.name + " Burned");
    }
}