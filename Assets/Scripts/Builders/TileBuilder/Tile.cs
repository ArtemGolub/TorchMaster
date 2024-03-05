using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public string Name;
    public int ID;

    public List<TileSO> NextTileVariants = new List<TileSO>();
    public List<ItemSO> PossibleItems = new List<ItemSO>();

    public List<CharacterSO> PossibleEnemies = new List<CharacterSO>();
    public List<Transform> CharacterSpawnPoints = new List<Transform>();
    
    public List<Transform> ItemSpawnPoints = new List<Transform>();
    public Dictionary<Transform, bool> NextTileSpawnPoints = new Dictionary<Transform, bool>();
}
