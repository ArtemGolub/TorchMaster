using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFirePoint
{
    bool burned { get; set; }
    void Burn();
}
