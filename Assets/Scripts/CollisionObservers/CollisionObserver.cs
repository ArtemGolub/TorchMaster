using System;
using System.Collections.Generic;
using UnityEngine;

public class CollisionObserver<T> : MonoBehaviour, ICollisionObserver<T>
{
    
    private Dictionary<string, ICollisionHandler<T>> collisionHandlers = new Dictionary<string, ICollisionHandler<T>>();
    
    public void AddCollisionHandler(string tag,ICollisionHandler<T> handler)
    {
        collisionHandlers.Add(tag, handler);
    }
    
    public void RemoveCollisionHandler(string tag)
    {
        if (collisionHandlers.ContainsKey(tag))
        {
            collisionHandlers.Remove(tag);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        string tag = other.gameObject.tag;
        if (collisionHandlers.ContainsKey(tag))
        {
            collisionHandlers[tag].HandleCollision(other.transform.GetComponent<T>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if (collisionHandlers.ContainsKey(tag))
        {
            collisionHandlers[tag].HandleCollision(other.transform.GetComponent<T>());
        }
    }

    private void OnCollisionExit(Collision other)
    {
        string tag = other.gameObject.tag;
        if (collisionHandlers.ContainsKey(tag))
        {
            collisionHandlers[tag].HandleCollisionExit(other.transform.GetComponent<T>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string tag = other.gameObject.tag;
        if (collisionHandlers.ContainsKey(tag))
        {
            collisionHandlers[tag].HandleCollisionExit(other.transform.GetComponent<T>());
        }
    }

    private void OnCollisionStay(Collision other)
    {
        string tag = other.gameObject.tag;
        if (collisionHandlers.ContainsKey(tag))
        {
            collisionHandlers[tag].HandleCollisionStay(other.transform.GetComponent<T>());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        string tag = other.gameObject.tag;
        if (collisionHandlers.ContainsKey(tag))
        {
            collisionHandlers[tag].HandleCollisionStay(other.transform.GetComponent<T>());
        }
    }
}