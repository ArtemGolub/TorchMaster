using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileFabric : MonoBehaviour
{
    public static TileFabric current;
    private Transform TileContainer;
    [SerializeField] private TileSO startTile;
    
    [SerializeField] private Transform spawnPose;

     [SerializeField]private List<MonoTile> deadEnds;
    private int levelTiles = 0;
    private int maxTiles = 25;
    private int triesToCreate = 10;

    public List<MonoTile> GeneratedTiles;

    private IEnumerator spawner;

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
    }

    private void Start()
    {
        CreateContainer();
        GenerateFirstTile(startTile, spawnPose);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RemoveLastTile();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            AddTile();
        }
    }

    private void RemoveLastTile()
    {
        var lastTile = GetLastTile();
        GeneratedTiles.Remove(lastTile);
        Destroy(lastTile.gameObject);
    }

    private void AddTile()
    {
        MonoTile lastTile = GetLastTile();
        GenerateNextTile(lastTile);
    }

    private void ReplaceLastTile()
    {
        triesToCreate -= 1;
        Debug.Log(triesToCreate);
        RemoveLastTile();
        AddTile();
    }

    private void CreateContainer()
    {
        GameObject containerObject = new GameObject("TileContainer");
        TileContainer = containerObject.transform;
    }

    private void GenerateFirstTile(TileSO startTile, Transform spawnPose)
    {
        var tile = Instantiate(startTile.prefab, spawnPose.position, spawnPose.rotation);
        tile.SetParent(TileContainer);
        GeneratedTiles.Add(tile.GetComponent<MonoTile>());

        AddTile();
    }


    private void GenerateNextTile(MonoTile tile)
    {
        if (levelTiles > maxTiles)
        {
            Debug.Log("Dungeon Generated!");
            SpawnDeadEnds();
            return;
        }

        EnqueueCoroutine(GenerateTileCor(tile));
    }

    private int RandomTileSOIndex(List<TileSO> list)
    {
        if (list == null) return 0;
        return Random.Range(0, list.Count);
    }

    private List<MonoTile.Directions> GetFreeDoors(List<MonoTile.Directions> doors)
    {
        if (doors == null || doors.Count == 0)
        {
            Debug.LogError("Нет доступных дверей для выбора.");
            return null;
        }

        List<MonoTile.Directions> freeDoors = new List<MonoTile.Directions>();
        for (int i = 0; i < doors.Count; i++)
        {
            if (doors[i].ConnectTo != null) continue;
            freeDoors.Add(doors[i]);
        }

        if (freeDoors.Count == 0)
        {
            for (int i = 0; i < GeneratedTiles.Count; i++)
            {
                freeDoors = GetPreviousDoor(GeneratedTiles[GeneratedTiles.Count - 1 - i]
                    .GetAllDirectionsByType(DirectionType.Door));
                if (freeDoors == null) continue;
                if (freeDoors.Count > 0)
                {
                    return freeDoors;
                }
            }

            Debug.LogError("Нет доступных свободных дверей для выбора.");
            return null;
        }

        return freeDoors;
    }

    private List<MonoTile.Directions> GetFreeWalls(List<MonoTile.Directions> walls)
    {
        if (walls == null || walls.Count == 0)
        {
            Debug.LogError("Нет доступных Стен для выбора.");
            return null;
        }

        List<MonoTile.Directions> freeWalls = new List<MonoTile.Directions>();
        for (int i = 0; i < walls.Count; i++)
        {
            if (walls[i].ConnectTo == null)
            {
                freeWalls.Add(walls[i]);
            }
        }

        return freeWalls;
    }

    private List<MonoTile.Directions> GetEmptyDoors(List<MonoTile.Directions> doors)
    {
        if (doors == null || doors.Count == 0)
        {
            Debug.LogError("Нет доступных Дверей для выбора.");
            return null;
        }

        List<MonoTile.Directions> freeWalls = new List<MonoTile.Directions>();
        for (int i = 0; i < doors.Count; i++)
        {
            if (doors[i].ConnectTo == null)
            {
                freeWalls.Add(doors[i]);
            }
        }

        return freeWalls;
    }

    private List<MonoTile.Directions> GetFreeEmptySpace(List<MonoTile.Directions> emptySpace)
    {
        if (emptySpace == null || emptySpace.Count == 0)
        {
            return null;
        }

        List<MonoTile.Directions> freeEmptySpace = new List<MonoTile.Directions>();
        for (int i = 0; i < emptySpace.Count; i++)
        {
            if (emptySpace[i].ConnectTo == null)
            {
                freeEmptySpace.Add(emptySpace[i]);
            }
        }

        return freeEmptySpace;
    }


    private List<MonoTile.Directions> GetPreviousDoor(List<MonoTile.Directions> directions)
    {
        List<MonoTile.Directions> doors = new List<MonoTile.Directions>();

        if (directions == null || directions.Count == 0)
        {
            Debug.LogError("Нет доступных дверей для выбора.");
            return null;
        }

        List<MonoTile.Directions> freeDoors = new List<MonoTile.Directions>();
        for (int i = 0; i < directions.Count; i++)
        {
            if (directions[i].ConnectTo != null) continue;
            freeDoors.Add(directions[i]);
        }

        return freeDoors;
    }

    private MonoTile.Directions GetRandomDoor(List<MonoTile.Directions> doors)
    {
        var freeDoors = GetFreeDoors(doors);
        var randomIndex = Random.Range(0, freeDoors.Count);
        return freeDoors[randomIndex];
    }

    private IEnumerator GenerateTileCor(MonoTile tile)
    {
        var tileDoors = tile.GetAllDirectionsByType(DirectionType.Door);
        MonoTile.Directions randomDoor = GetRandomDoor(tileDoors);
        Transform spawnPoint = randomDoor.transform;
        TileSO randomTileSO = tile._tile.NextTileVariants[RandomTileSOIndex(tile._tile.NextTileVariants)];

        if (triesToCreate <= 0)
        {
            Debug.Log("Dead End");
            SpawnDeadEnd(tile, randomDoor);
        }
        
        yield return new WaitForSeconds(0.1f);

        GameObject newTileObject = CreateNewTile(randomTileSO, spawnPoint.position, spawnPoint.rotation);
        MonoTile generatedTile = newTileObject.GetComponent<MonoTile>();
        GeneratedTiles.Add(generatedTile);


        if (generatedTile.GetDoorByDirection(generatedTile.ReverseDirection(randomDoor.direction)) == null)
        {
            generatedTile = null;
            Debug.Log("Need to replace: Door Direction");
            ReplaceLastTile();
            yield break;
        }
        
        generatedTile.GetDoorByDirection(generatedTile.ReverseDirection(randomDoor.direction)).ConnectTo =
            randomDoor.transform;

        if (randomDoor.directionType != generatedTile
                .GetDoorByDirection(generatedTile.ReverseDirection(randomDoor.direction)).directionType)
        {
            generatedTile = null;
            Debug.Log("Need to replace: Direction Type");
            ReplaceLastTile();
            yield break;
        }
        
        if (IsCollideWithOtherTiles(generatedTile))
        {
            generatedTile = null;
            Debug.Log("Need to replace: Collision");
            ReplaceLastTile();
            yield break;
        }
        
        foreach (var dir in generatedTile.directions)
        {
            if (dir.directionType == DirectionType.Door)
            {
                if (dir.ConnectTo != null)
                {
                    string connectionName = dir.ConnectTo.name;
                    if (connectionName.Length >= 4)
                    {
                        string lastFourCharacters = connectionName.Substring(connectionName.Length - 4);
                
                        if (lastFourCharacters == "Wall")
                        {
                            Debug.Log("Имя объекта заканчивается на 'Wall'");
                            ReplaceLastTile();
                            yield break;
                        }
                    }
                }
            }
        }
        
        
        if (generatedTile != null)
        {
            randomDoor.ConnectTo = generatedTile
                .GetDoorByDirection(generatedTile.ReverseDirection(randomDoor.direction)).transform;
            levelTiles++;
            AddTile();
            triesToCreate = 10;
        }
    }

    bool IsCollideWithOtherTiles(MonoTile tile)
    {
        var tileDoors = tile.GetAllDirectionsByType(DirectionType.Door);
        List<MonoTile.Directions> freeDors = GetEmptyDoors(tileDoors);
        if (ColldeWithDoors(freeDors))
        {
            Debug.Log("Doors failed check");
            return true;
        }

        var tileWalls = tile.GetAllDirectionsByType(DirectionType.Wall);
        List<MonoTile.Directions> freeWalls = GetFreeWalls(tileWalls);
        if (CheckCollisionWithWalls(freeWalls))
        {
            Debug.Log("Walls failed check");
            return true;
        }

        var freeSpace = tile.GetAllDirectionsByType(DirectionType.Empty);
        if (GetFreeEmptySpace(freeSpace) != null)
        {
            if (CheckCollideWithEmpty(GetFreeEmptySpace(freeSpace)))
            {
                Debug.Log("Empty space failed check");
                return true;
            }
        }


        return false;
    }

    private bool CheckCollideWithEmpty(List<MonoTile.Directions> freeEmptySpace)
    {
        List<MonoTile.Directions> emptySpaceToCheck = new List<MonoTile.Directions>();
        List<MonoTile> collidedTilesToCheck = new List<MonoTile>();
        foreach (var wall in freeEmptySpace)
        {
            Collider[] colliders = Physics.OverlapSphere(wall.transform.position, 2);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Tile"))
                {
                    emptySpaceToCheck.Add(wall);
                    collidedTilesToCheck.Add(collider.transform.GetComponentInParent<MonoTile>());
                }
            }
        }

        return CollideWithEmpty(emptySpaceToCheck, collidedTilesToCheck);
    }

    private bool WallColliderWithDoor(List<MonoTile.Directions> doorsToCheck, List<MonoTile> collidedTilesToCheck)
    {
        for (int i = 0; i < doorsToCheck.Count; i++)
        {
            List<MonoTile.Directions> freeWalls =
                GetFreeWalls(collidedTilesToCheck[i].GetAllDirectionsByType(DirectionType.Wall));
            if (freeWalls == null)
            {
                return false;
            }

            foreach (var wall in freeWalls)
            {
                if (wall.direction == collidedTilesToCheck[i].ReverseDirection(doorsToCheck[i].direction))
                {
                    wall.ConnectTo = doorsToCheck[i].transform;
                    doorsToCheck[i].ConnectTo = wall.transform;
                    return true;
                }
            }
        }

        return false;
    }

    private bool CoolideWithWawlls(List<MonoTile.Directions> wallToCheck, List<MonoTile> collidedTilesToCheck)
    {
        for (int i = 0; i < wallToCheck.Count; i++)
        {
            List<MonoTile.Directions> freeWalls =
                GetFreeWalls(collidedTilesToCheck[i].GetAllDirectionsByType(DirectionType.Wall));
            if (freeWalls == null)
            {
                return true;
            }

            foreach (var wall in freeWalls)
            {
                if (wall.direction == collidedTilesToCheck[i].ReverseDirection(wallToCheck[i].direction))
                {
                    wall.ConnectTo = wallToCheck[i].transform;
                    wallToCheck[i].ConnectTo = wall.transform;
                    //return false;
                }
            }
        }

        return true;
    }

    private bool ColldeWithDoors(List<MonoTile.Directions> freeDors)
    {
        List<MonoTile.Directions> doorsToCheck = new List<MonoTile.Directions>();
        List<MonoTile> colidedDootsToCheck = new List<MonoTile>();
        foreach (var door in freeDors)
        {
            Collider[] colliders = Physics.OverlapSphere(door.transform.position, 2);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Tile"))
                {
                    doorsToCheck.Add(door);
                    colidedDootsToCheck.Add(collider.transform.GetComponentInParent<MonoTile>());
                }
            }
        }

        var dobuleDoor = CheckDubleDoor(doorsToCheck, colidedDootsToCheck);
        var doorCollideWithWall = WallColliderWithDoor(doorsToCheck, colidedDootsToCheck);

        var condition = dobuleDoor && doorCollideWithWall;
        Debug.Log("DoorCollide With wall: " + doorCollideWithWall + " condition: " + condition);
        return condition;
    }

    private bool CheckCollisionWithWalls(List<MonoTile.Directions> freeWalls)
    {
        List<MonoTile.Directions> wallsToCheck = new List<MonoTile.Directions>();
        List<MonoTile> collidedTilesToCheck = new List<MonoTile>();
        foreach (var wall in freeWalls)
        {
            Collider[] colliders = Physics.OverlapSphere(wall.transform.position, 2);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Tile"))
                {
                    wallsToCheck.Add(wall);
                    collidedTilesToCheck.Add(collider.transform.GetComponentInParent<MonoTile>());
                }
            }
        }

        var collidedWithEmptyTile = CollideWithEmpty(wallsToCheck, collidedTilesToCheck);
        var collidedWithDoor = CheckDoorCollision(wallsToCheck, collidedTilesToCheck);
        var collidedWithOtherWalls = CoolideWithWawlls(wallsToCheck, collidedTilesToCheck);

        bool condition = collidedWithEmptyTile && collidedWithDoor && collidedWithOtherWalls;
        return !condition;
    }

    private bool CheckDoorCollision(List<MonoTile.Directions> wallsToCheck, List<MonoTile> collidedTilesToCheck)
    {
        for (int i = 0; i < wallsToCheck.Count; i++)
        {
            List<MonoTile.Directions> getFreeDoors =
                GetEmptyDoors(collidedTilesToCheck[i].GetAllDirectionsByType(DirectionType.Door));
            if (getFreeDoors == null)
            {
                return true;
            }

            foreach (var freeDoor in getFreeDoors)
            {
                if (freeDoor.direction == collidedTilesToCheck[i].ReverseDirection(wallsToCheck[i].direction))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool CollideWithEmpty(List<MonoTile.Directions> wallsToCheck, List<MonoTile> collidedTilesToCheck)
    {
        if (wallsToCheck == null || collidedTilesToCheck == null)
        {
            return true;
        }

        if (wallsToCheck.Count == 0 || collidedTilesToCheck.Count == 0)
        {
            return true;
        }

        for (int i = 0; i < wallsToCheck.Count; i++)
        {
            List<MonoTile.Directions> freeEmptySpace =
                GetFreeEmptySpace(collidedTilesToCheck[i].GetAllDirectionsByType(DirectionType.Empty));
            if (freeEmptySpace == null)
            {
                return true;
            }

            foreach (var emptySpace in freeEmptySpace)
            {
                if (emptySpace.direction == collidedTilesToCheck[i].ReverseDirection(wallsToCheck[i].direction))
                {
                    wallsToCheck[i].ConnectTo = emptySpace.transform;
                    emptySpace.ConnectTo = wallsToCheck[i].transform;

                    if (wallsToCheck[i].directionType != emptySpace.directionType)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool CheckDubleDoor(List<MonoTile.Directions> doorsToCheck, List<MonoTile> collidedTile)
    {
        for (int i = 0; i < doorsToCheck.Count; i++)
        {
            List<MonoTile.Directions> freeDoors =
                GetEmptyDoors(collidedTile[i].GetAllDirectionsByType(DirectionType.Door));
            if (freeDoors == null)
            {
                return false;
            }

            foreach (var collidedTileDoor in freeDoors)
            {
                if (collidedTileDoor.direction == collidedTile[i].ReverseDirection(doorsToCheck[i].direction))
                {
                    doorsToCheck[i].ConnectTo = collidedTileDoor.transform;
                    collidedTileDoor.ConnectTo = doorsToCheck[i].transform;
                    if (doorsToCheck[i].directionType != collidedTileDoor.directionType)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private MonoTile GetLastTile()
    {
        var lastIndex = GeneratedTiles.Count - 1;
        MonoTile lassTile = GeneratedTiles[lastIndex];
        return lassTile;
    }

    private GameObject CreateNewTile(TileSO tileSO, Vector3 position, Quaternion rotation)
    {
        GameObject newTile = Instantiate(tileSO.prefab.gameObject, position, rotation);
        newTile.transform.SetParent(TileContainer);
        return newTile;
    }

    private void SpawnDeadEnds()
    {
        foreach (var tile in GeneratedTiles)
        {
            foreach (var door in tile.GetAllDirectionsByType(DirectionType.Door))
            {
                if (door.ConnectTo == null)
                {
                    SpawnDeadEnd(tile, door);
                }
            }
        }
        
    }
    
    private void SpawnDeadEnd(MonoTile tile,MonoTile.Directions door)
    {
        MonoTile deadEnd = null;
        
        for (int i = deadEnds.Count - 2; i >= 0; i--)
        {
            foreach (var dir in deadEnds[i].GetAllDirectionsByType(DirectionType.Door))
            {
                if (dir.direction == tile.ReverseDirection(door.direction))
                {
                    deadEnd = deadEnds[i];
                    
                    var deadEndTile = Instantiate(deadEnd, door.transform.position, door.transform.rotation);
                    deadEndTile.transform.SetParent(TileContainer);

                    foreach (var direct in deadEndTile.GetAllDirectionsByType(DirectionType.Door))
                    {
                        if (direct == dir)
                        {
                            door.ConnectTo = direct.transform;
                            direct.ConnectTo = door.transform;
                        }
                    }
                }
            }
        }
    }
    
    private Queue<IEnumerator> coroutineQueue = new Queue<IEnumerator>();

    private void EnqueueCoroutine(IEnumerator coroutine)
    {
        coroutineQueue.Enqueue(coroutine);
        if (coroutineQueue.Count == 1)
        {
            StartCoroutine(DequeueCoroutine());
        }
    }

    private IEnumerator DequeueCoroutine()
    {
        while (coroutineQueue.Count > 0)
        {
            yield return StartCoroutine(coroutineQueue.Peek());
            coroutineQueue.Dequeue();
        }
    }
}