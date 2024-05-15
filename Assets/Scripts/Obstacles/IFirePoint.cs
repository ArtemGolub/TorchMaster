using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFirePoint
{
    Transform _transform { get; set; }
    bool burned { get; set; }
    void Burn();
}
