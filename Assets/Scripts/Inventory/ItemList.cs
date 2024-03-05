using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemList<T>: List<T>
{
    public int _maxSize;

    public ItemList(int maxSize)
    {
        if (maxSize <= 0)
        {
            Debug.LogWarning("Max Size not set");
        }

        _maxSize = maxSize;
    }

    public new void Add(T item)
    {
        if (Count < _maxSize)
        {
            base.Add(item);
        }
        else
        {
           Debug.LogWarning("Max Items");
        }
    }
}