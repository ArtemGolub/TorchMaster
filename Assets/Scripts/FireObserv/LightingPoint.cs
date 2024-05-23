using System;
using UnityEngine;

public class LightingPoint :MonoBehaviour, ILightingPoint
{
    public Light myLight { get; set; }
    [SerializeField] private bool isEnabled;
    
    private void OnEnable()
    {
        myLight = GetComponent<Light>();

        if (myLight == null)
        {
            Debug.LogError("Light component is not assigned to myLight.");
        }
        else
        {
            var lightRange = myLight.range / 2;
            var collider = GetComponent<SphereCollider>();
            collider.radius = lightRange;
            myLight.enabled = isEnabled;
        }
    }
    
    public void DisableCollider()
    {
        //transform.GetComponent<Collider>().enabled = false;
    }

    public void EnableLighting()
    {
        if (myLight != null)
        {
            myLight.enabled = true;
        }
        else
        {
            Debug.LogError("Light component is not assigned to myLight.");
        }
    }
}