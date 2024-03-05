using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "Tiles/Tile", order = 1)]
public class TileSO : ScriptableObject
{
    public string Name;
    public int ID;

    public Transform prefab;

    [Header("Items")]
    public List<ItemSO> possibleItems;
    
    [Header("Enemies")]
    public List<CharacterSO> possibleEnimies;
    
    [Header("Next Tile")]
    public List<TileSO> NextTileVariants;
}