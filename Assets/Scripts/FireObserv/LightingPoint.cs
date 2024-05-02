using System;
using UnityEngine;

public class LightingPoint :MonoBehaviour, ILightingPoint
{
    public void DisableCollider()
    {
       // transform.GetComponent<Collider>().enabled = false;
    }

    public void EnableCollider()
    {
       // transform.GetComponent<Collider>().enabled = true;
    }
}