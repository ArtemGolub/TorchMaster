using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemList<T>: List<T>
{
    private int _maxSize;

    public ItemList(int maxSize)
    {
        if (maxSize <= 0)
        {
            Debug.LogWarning("Max Size not set");
        }

        _maxSize = maxSize;
    }

    public new bool Add(T item)
    {
        if (Count < _maxSize)
        {
            base.Add(item);
            return true;
        }
        else
        {
            Debug.LogWarning("Max Items");
            return false;
        }
    }
}