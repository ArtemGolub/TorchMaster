using System.Collections.Generic;
using UnityEngine;

public sealed class RoomContentGenerator : MonoBehaviour
{
    public DungeonContentSO DungeonContentSo;
    public List<Transform> obstacle;
    
    private int maxEnimies;
    private int maxItems;
    private int maxKeys = 1;
    
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


    
    public RoomContent SetContent(RoomSO roomData, List<Transform> ItemSpawnPoints, List<Transform> EnemiesSpawnPoint, Dictionary<Transform, bool> allSpawnPoints)
    {
        _roomDirector = new RoomDirector();
        RoomContent roomContent = _roomDirector.CreateRoomContent(roomData, ItemSpawnPoints, EnemiesSpawnPoint, allSpawnPoints);
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
            TrySpawnEnemies(roomContent.PossibleEnemies, roomContent.SpawnPoints);
        }
        else if (randomNumber <= 50)
        {
            TrySpawnItem(roomContent.PossbileItems, roomContent.SpawnPoints);
        }
        else if (randomNumber <= 100)
        {
            TrySpawnObstacles(roomContent.SpawnPoints);
        }
    }
    
    private bool TryGetSpawnPoint(out Transform spawnPoint, Dictionary<Transform, bool> spawnPoints)
    {
        spawnPoint = null;
        
        List<Transform> availableSpawnPoints = new List<Transform>();
        foreach (var kvp in spawnPoints)
        {
            if (!kvp.Value)
            {
                availableSpawnPoints.Add(kvp.Key);
            }
        }
        
        if (availableSpawnPoints.Count == 0)
        {
            return false;
        }
        
        int randomIndex = UnityEngine.Random.Range(0, availableSpawnPoints.Count);
        spawnPoint = availableSpawnPoints[randomIndex];
        return true;
    }
    
    private void MarkSpawnPointUsed(Transform spawnPoint, Dictionary<Transform, bool> spawnPoints)
    {
        if (spawnPoints.ContainsKey(spawnPoint))
        {
            spawnPoints[spawnPoint] = true; // Помечаем спаунпоинт как использованный
        }
    }
    
    private void TrySpawnEnemies(List<CharacterSO> possibleEnemies, Dictionary<Transform, bool> spawnPoints)
    {
        Transform chosenSpawnPoint = null;
        if (TryGetSpawnPoint(out chosenSpawnPoint, spawnPoints))
        {
            if (maxEnimies <= 0) return;
            if(possibleEnemies == null) return;
            CharacterSO character = possibleEnemies[GenerateRandomNumber(0, possibleEnemies.Count)];
            CharacterFabric.current.SpawnCharacterAtPoint(character, chosenSpawnPoint);
            maxEnimies -= 1;
            MarkSpawnPointUsed(chosenSpawnPoint, spawnPoints);
        }
        else
        {
            // Логика для случая, когда все спаунпоинты заняты
        }
    }
    private void TrySpawnObstacles(Dictionary<Transform, bool> spawnPoints)
    {
        Transform chosenSpawnPoint = null;
        if (TryGetSpawnPoint(out chosenSpawnPoint, spawnPoints))
        {
            var obstacleToGenerate = obstacle[GenerateRandomNumber(0, obstacle.Count)];
            var obj = Instantiate(obstacleToGenerate, chosenSpawnPoint.position - new Vector3(0,0.9f,0), chosenSpawnPoint.rotation);
            MarkSpawnPointUsed(chosenSpawnPoint, spawnPoints);
        }
        else
        {
            // Логика для случая, когда все спаунпоинты заняты
        }
    }
    
    private void TrySpawnItem(List<ItemSO> possibleItems, Dictionary<Transform, bool> spawnPoints)
    {
        Transform chosenSpawnPoint = null;
        var possibleItemList = possibleItems;
        if (TryGetSpawnPoint(out chosenSpawnPoint, spawnPoints))
        {
            if (maxItems <= 0) return;
            if(possibleItemList == null) return;
            ItemSO item = possibleItemList[GenerateRandomNumber(0, possibleItems.Count)];

            if (item.itemType == ItemType.Key)
            {
                if (maxKeys == 0)
                {
                    possibleItemList.Remove(item);
                    item = possibleItemList[GenerateRandomNumber(0, possibleItems.Count)];
                }
                else
                {
                    maxKeys--;
                    Debug.Log("Key Spawned");
                }
            }
            else
            {
                maxItems -= 1;
            }
            ItemFabric.current.SpawnItem(item, chosenSpawnPoint);
            MarkSpawnPointUsed(chosenSpawnPoint, spawnPoints);
        }
    }
    
    // private void TrySpawnEnemies(List<CharacterSO> possibleEnemies, List<Transform> enemiesSpawnPoint)
    // {
    //     if (maxEnimies <= 0) return;
    //     if(possibleEnemies == null) return;
    //     if(enemiesSpawnPoint.Count <= 0) return;
    //     
    //     CharacterSO character = possibleEnemies[GenerateRandomNumber(0, possibleEnemies.Count)];
    //     Transform spawnPoint = enemiesSpawnPoint[GenerateRandomNumber(0, enemiesSpawnPoint.Count)];
    //     CharacterFabric.current.SpawnCharacterAtPoint(character, spawnPoint);
    //     maxEnimies -= 1;
    // }
    //
    // private void TrySpawnItem(List<ItemSO> possibleItems, List<Transform> itemSpawnPoints)
    // {
    //     if(maxItems <= 0) return;
    //     if(possibleItems == null) return;
    //     if(itemSpawnPoints.Count <= 0) return;
    //     
    //     ItemSO item = possibleItems[GenerateRandomNumber(0, possibleItems.Count)];
    //     Transform spawnPoint =  itemSpawnPoints[GenerateRandomNumber(0, itemSpawnPoints.Count)];
    //     ItemFabric.current.SpawnItem(item, spawnPoint);
    //     maxItems -= 1;
    // }
    //
    // private void TrySpawnObstacle( List<Transform> itemSpawnPoint)
    // {
    //     if(itemSpawnPoint.Count <= 0) return;
    //     Transform spawnPoint =  itemSpawnPoint[GenerateRandomNumber(0, itemSpawnPoint.Count)];
    //     var obj = Instantiate(obstacle, spawnPoint.position - new Vector3(0,0.9f,0), spawnPoint.rotation);
    // }
    
    private int GenerateRandomNumber(int min, int max)
    {
        return Random.Range(min, max - 1);
    }
    
}
