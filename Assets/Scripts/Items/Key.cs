using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : AItem
{
    private void Start()
    {
        InitItem();
        item.FSM.InitBehaviour();
    }
    
    private void Update()
    {
        item.FSM.UpdateBehaviour();
    }
}
