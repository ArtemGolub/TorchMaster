using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour, IFirePoint
{
    [SerializeField]private List<Transform> firePoints;
    public bool burned { get; set; }

    public void Burn()
    {
        foreach (var firePoint in firePoints)
        {
            firePoint.gameObject.SetActive(true);
        }
        LightingPoint lightingPoint = GetComponentInChildren<LightingPoint>();
        if (lightingPoint != null)
        {
            lightingPoint.EnableLighting();
        }
        else
        {
            Debug.LogError("LightingPoint component not found.");
        }

        burned = true;
    }
}
