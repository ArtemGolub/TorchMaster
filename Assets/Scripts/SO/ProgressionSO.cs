using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Progress", menuName = "Progression/Level Progress", order = 1)]
public class ProgressionSO : ScriptableObject
{
    public List<string> LevelName;
    public List<LevelState> LevelState;
}