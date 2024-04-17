using System.Collections.Generic;
using UnityEngine;

public sealed class RoomContentGenerator : MonoBehaviour
{
    public DungeonContentSO DungeonContentSo;
    public Transform obstacle;
    
    private int maxEnimies;
    private int maxItems;
    
    public static RoomContentGenerator current;
    private RoomDirector _roomDirector;
    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        if (current != this)
        {
            Destroy(transform);
        }

        SetParams();
    }
        
    public RoomContent SetContent(RoomSO roomData, List<Transform> ItemSpawnPoints, List<Transform> EnemiesSpawnPoint)
    {
        _roomDirector = new RoomDirector();
        RoomContent roomContent = _roomDirector.CreateRoomContent(roomData, ItemSpawnPoints, EnemiesSpawnPoint);
        return roomContent;
    }

    private void SetParams()
    {
        maxEnimies = DungeonContentSo.maxEnimies;
        maxItems = DungeonContentSo.maxItems;
    }
    
    public void CreateContent(RoomContent roomContent)
    {
        if(roomContent.ContentCapacity == 0) return;
        for (int i = 0; i < roomContent.ContentCapacity; i++)
        {
            TrySpawnContent(roomContent);
        }
    }

    private void TrySpawnContent(RoomContent roomContent)
    {
        int randomNumber = GenerateRandomNumber(0, 100);
        if (randomNumber <= 20)
        {
            TrySpawnEnemies(roomContent.PossibleEnemies, roomContent.EnemiesSpawnPoints);
        }
        else if (randomNumber <= 50)
        {
            TrySpawnItem(roomContent.PossbileItems, roomContent.ItemSpawnPoints);
        }
        else if (randomNumber <= 100)
        {
            TrySpawnObstacle(roomContent.ItemSpawnPoints);
        }
    }
    
    private void TrySpawnEnemies(List<CharacterSO> possibleEnemies, List<Transform> enemiesSpawnPoint)
    {
        if (maxEnimies <= 0) return;
        if(possibleEnemies == null) return;
        if(enemiesSpawnPoint.Count <= 0) return;
        
        CharacterSO character = possibleEnemies[GenerateRandomNumber(0, possibleEnemies.Count)];
        Transform spawnPoint = enemiesSpawnPoint[GenerateRandomNumber(0, enemiesSpawnPoint.Count)];
        CharacterFabric.current.SpawnCharacterAtPoint(character, spawnPoint);
        maxEnimies -= 1;
    }

    private void TrySpawnItem(List<ItemSO> possibleItems, List<Transform> itemSpawnPoints)
    {
        if(maxItems <= 0) return;
        if(possibleItems == null) return;
        if(itemSpawnPoints.Count <= 0) return;
        
        ItemSO item = possibleItems[GenerateRandomNumber(0, possibleItems.Count)];
        Transform spawnPoint =  itemSpawnPoints[GenerateRandomNumber(0, itemSpawnPoints.Count)];
        ItemFabric.current.SpawnItem(item, spawnPoint);
        maxItems -= 1;
    }
    
    private void TrySpawnObstacle( List<Transform> itemSpawnPoint)
    {
        if(itemSpawnPoint.Count <= 0) return;
        Transform spawnPoint =  itemSpawnPoint[GenerateRandomNumber(0, itemSpawnPoint.Count)];
        var obj = Instantiate(obstacle, spawnPoint.position - new Vector3(0,0.9f,0), spawnPoint.rotation);
    }
    
    private int GenerateRandomNumber(int min, int max)
    {
        return Random.Range(min, max - 1);
    }
    
}
