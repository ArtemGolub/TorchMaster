using System.Collections.Generic;
using UnityEngine;

public class RoomDirector
{
    private IRoomBuilder _roomBuilder;

    public RoomContent CreateRoomContent(RoomSO roomData, List<Transform> ItemSpawnPoints,
        List<Transform> EnemiesSpawnPoints)
    {
        _roomBuilder = new RoomBuilder();

        SetParams(roomData);
        SetSpawnPoints(ItemSpawnPoints, EnemiesSpawnPoints);

        RoomContent roomContent = _roomBuilder.GetRoom();
        return roomContent;
    }

    private void SetParams(RoomSO roomData)
    {
        _roomBuilder.SetPossibleItems(roomData.possibleItems);
        _roomBuilder.SetPossibleEnemies(roomData.possibleEnimies);
        _roomBuilder.SetContentCapacity(roomData.ContentCapacity);
    }

    private void SetSpawnPoints(List<Transform> ItemSpawnPoints, List<Transform> EnemiesSpawnPoints)
    {
        _roomBuilder.SetItemSpawnPoints(ItemSpawnPoints);
        _roomBuilder.SetEnemiesSpawnPoints(EnemiesSpawnPoints);
    }
}