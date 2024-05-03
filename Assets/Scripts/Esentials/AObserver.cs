using System.Collections.Generic;
using UnityEngine;

public abstract class AObserver<T> : MonoBehaviour
{
    public List<T> observers = new List<T>();
    
    public void AddObserver(T observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(T observer)
    {
        observers.Remove(observer);
    }

    public bool ContainObserver(T observer)
    {
        if (observers.Contains(observer))
        {
            return true;
        }
        return false;
    }
}
