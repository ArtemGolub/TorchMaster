using UnityEngine;

public interface ILightingPoint
{
    Light myLight { get; set; }
    void DisableCollider();
    void EnableLighting();
}