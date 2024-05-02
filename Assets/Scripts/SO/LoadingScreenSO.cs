using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loading Screen", menuName = "UI/LoadingScreen", order = 1)]
public class LoadingScreenSO : ScriptableObject
{
    public List<String> Hints;
    public List<Sprite> mainImages;
}
