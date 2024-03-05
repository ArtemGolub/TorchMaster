using UnityEngine;

public sealed class Oil : AItem, IItem
{
    private void Awake()
    {
        InitItem();
    }
    private void Start()
    {
        item.FSM.InitBehaviour();
    }
    
    private void Update()
    {
        item.FSM.UpdateBehaviour();
    }
}