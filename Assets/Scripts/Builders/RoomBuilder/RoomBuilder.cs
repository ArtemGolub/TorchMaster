using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : IRoomBuilder
{
    private RoomContent _roomContent = new RoomContent();
    
    public RoomContent GetRoom()
    {
        return _roomContent;
    }
    public void SetPossibleItems(List<ItemSO> itemsData)
    {
        if(itemsData == null) return;
        _roomContent.PossbileItems = itemsData;
    }
    public void SetPossibleEnemies(List<CharacterSO> enemiesData)
    {
        if(enemiesData == null) return;
        _roomContent.PossibleEnemies = enemiesData;
    }

    public void SetContentCapacity(int capacity)
    {
        _roomContent.ContentCapacity = capacity;
    }

    public void SetItemSpawnPoints(List<Transform> ItemSpawnPoints)
    {
        if(ItemSpawnPoints == null) return;
        _roomContent.ItemSpawnPoints = ItemSpawnPoints;
    }

    public void SetEnemiesSpawnPoints(List<Transform> EnemiesSpawnPoints)
    {
        if(EnemiesSpawnPoints == null) return;
        _roomContent.EnemiesSpawnPoints = EnemiesSpawnPoints;
    }

    public void SetAllSpawnPoints()
    {
        _roomContent.SpawnPoints = new Dictionary<Transform, bool>();
        // foreach (var itemSpawnPoint in _roomContent.ItemSpawnPoints)
        // {
        //     AllSpawnPoints.Add(itemSpawnPoint, false);
        // }
        if(_roomContent.EnemiesSpawnPoints == null) return;
        foreach (var enemySpawnPoint in _roomContent.EnemiesSpawnPoints)
        {
            _roomContent.SpawnPoints.Add(enemySpawnPoint, false);
        }
    }
}
