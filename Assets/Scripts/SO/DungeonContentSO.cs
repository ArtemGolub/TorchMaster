using UnityEngine;

[CreateAssetMenu(fileName = "New DungeonContent", menuName = "Dungeons/Content", order = 1)]
public class DungeonContentSO : ScriptableObject
{
    public string Name;
    public int maxEnimies;
    public int maxItems;
}
