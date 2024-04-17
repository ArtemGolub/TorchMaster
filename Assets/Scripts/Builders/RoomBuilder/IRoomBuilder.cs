using System.Collections.Generic;
using UnityEngine;

public interface IRoomBuilder
{
    RoomContent GetRoom();
    void SetPossibleItems(List<ItemSO> itemsData);
    void SetPossibleEnemies(List<CharacterSO> charactersData);
    void SetContentCapacity(int capacity);
    void SetItemSpawnPoints(List<Transform> ItemSpawnPoints);
    void SetEnemiesSpawnPoints(List<Transform> EnemiesSpawnPoints);
}
