using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile", menuName = "Tiles/Tile", order = 1)]
public class RoomSO : ScriptableObject
{
    [Header("Content")] 
    public int ContentCapacity;

    [Header("Items")]
    public bool isStartRoom;
    public List<ItemSO> possibleItems;
    [Header("Enemies")]
    public List<CharacterSO> possibleEnimies;
}