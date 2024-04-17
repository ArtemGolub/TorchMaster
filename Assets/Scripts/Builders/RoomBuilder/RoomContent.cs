using System.Collections.Generic;
using UnityEngine;

public sealed class RoomContent : IRoomContent
{
    public int ContentCapacity { get; set; }
    public List<ItemSO> PossbileItems { get; set; }
    public List<CharacterSO> PossibleEnemies { get; set; }
    public List<Transform> ItemSpawnPoints { get; set; }
    public List<Transform> EnemiesSpawnPoints { get; set; }
    
}