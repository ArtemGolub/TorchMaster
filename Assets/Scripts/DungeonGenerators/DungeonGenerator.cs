using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonGenerator : MonoBehaviour
{
    public LayerMask roomMask;
    public List<DungeonRoom> roomPrefabs;
    public Vector2Int mapSize;
    private DungeonRoom[,] spawnedRooms;

    private Queue<Vector2Int> recalculatePossibleRoomsQueue = new Queue<Vector2Int>();
    private List<DungeonRoom>[,] possibleRooms;
    public DungeonRoom startRoom;
    public DungeonRoom endRoom;
    public DungeonRoom darkRoom;

    private Grid _grid;
    private PathFinder _pathFinder;
    private Vector3 cellSize;


    private void Start()
    {
        spawnedRooms = new DungeonRoom[mapSize.x, mapSize.y];

        CreateRotatedRooms();
        Generate();

        _grid = GetComponent<Grid>();
        _pathFinder = GetComponent<PathFinder>();


        StartCoroutine(PathCheck());
    }

    private IEnumerator PathCheck()
    {
        yield return new WaitForSeconds(0.3f);

        _grid.SetGrid(mapSize.x - 2, mapSize.y - 2, spawnedRooms[mapSize.x / 2, mapSize.y / 2].transform, cellSize);

        yield return new WaitForSeconds(0.3f);

        var pathFound = _pathFinder.GetPath(spawnedRooms[1, 1].transform,
            spawnedRooms[mapSize.x - 2, mapSize.y - 2].transform, _grid);

        int maxAttempts = 50;
        int attempts = 0;

        while (!pathFound && attempts < maxAttempts)
        {
            var closestNode = _pathFinder.FindClosestNodeToTarget();
            Collider[] colliders = Physics.OverlapSphere(closestNode.worldPosition, 0.5f, roomMask);
            var roomPosition = colliders[0].GetComponentInParent<DungeonRoom>().gridPosition;
            Debug.Log(roomPosition);
            
            List<DungeonRoom> roomsToRegenerate = new List<DungeonRoom>();
            
            for (int x = roomPosition.x - 1; x < mapSize.x-1; x++)
            {
                for (int y = roomPosition.y - 1; y < mapSize.y-1; y++)
                {
                    var borderCondition = x == 0 || y == 0 ||
                                          x == mapSize.x - 1 || y == mapSize.y - 1;
                    var startTileCondition = new Vector2Int(x, y) == new Vector2Int(1, 1);
                    var endTileCondition =  new Vector2Int(x, y) == new Vector2Int(mapSize.x - 2, mapSize.y - 2);
                    if (borderCondition || startTileCondition || endTileCondition)
                    {
                        Debug.Log($"{borderCondition} {startTileCondition} {endTileCondition}");
                        continue;
                    }
                    roomsToRegenerate.Add(spawnedRooms[x, y]);
                    Debug.Log($"added: {x} - {y}");
                }
            }

            Debug.Log(roomsToRegenerate.Count);
            yield return new WaitForSeconds(0.2f);
            ReGenerate(roomsToRegenerate);
            yield return new WaitForSeconds(0.2f);
            _grid.SetGrid(mapSize.x - 2, mapSize.y - 2, spawnedRooms[mapSize.x / 2, mapSize.y / 2].transform, cellSize);
            yield return new WaitForSeconds(0.2f);
            pathFound = _pathFinder.GetPath(spawnedRooms[1, 1].transform,
                spawnedRooms[mapSize.x - 2, mapSize.y - 2].transform, _grid);
            Debug.Log($"trying to change times: {attempts}");
            attempts++;
        }

        if (!pathFound)
        {
            Debug.Log("Path not found after 50 attempts.");
            yield break;
        }
        //Debug.Log("Path found.");
        yield break;
    }

    private void CreateRotatedRooms()
    {
        var countBeforeAdding = roomPrefabs.Count;
        DungeonRoom clone;
        for (int i = 0; i < countBeforeAdding; i++)
        {
            switch (roomPrefabs[i].Rotation)
            {
                case DungeonRoom.RotationType.OnlyRotation:
                    break;
                case DungeonRoom.RotationType.TwoRotations:
                    roomPrefabs[i].Weight /= 2;
                    if (roomPrefabs[i].Weight <= 0) roomPrefabs[i].Weight = 1;

                    clone = Instantiate(roomPrefabs[i], transform.position + Vector3.right * -200, Quaternion.identity);
                    clone.RotateRoom90();
                    clone.transform.name += " +90";
                    roomPrefabs.Add(clone);
                    break;
                case DungeonRoom.RotationType.FourRotations:
                    roomPrefabs[i].Weight /= 4;
                    if (roomPrefabs[i].Weight <= 0) roomPrefabs[i].Weight = 1;

                    clone = Instantiate(roomPrefabs[i], transform.position + Vector3.right * -200, Quaternion.identity);
                    clone.RotateRoom90();
                    clone.transform.name += " +90";
                    roomPrefabs.Add(clone);

                    clone = Instantiate(roomPrefabs[i], transform.position + Vector3.right * -200, Quaternion.identity);
                    clone.RotateRoom90();
                    clone.RotateRoom90();
                    clone.transform.name += " +180";
                    roomPrefabs.Add(clone);

                    clone = Instantiate(roomPrefabs[i], transform.position + Vector3.right * -200, Quaternion.identity);
                    clone.RotateRoom90();
                    clone.RotateRoom90();
                    clone.RotateRoom90();
                    clone.transform.name += " -90";
                    roomPrefabs.Add(clone);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            RegenerateDungeon();
        }
    }

    private void RegenerateDungeon()
    {
        foreach (DungeonRoom room in spawnedRooms)
        {
            if (room != null) Destroy(room.gameObject);
        }

        Generate();

        StartCoroutine(PathCheck());
    }


    private void Generate()
    {
        possibleRooms = new List<DungeonRoom>[mapSize.x, mapSize.y];

        int maxAttempts = 10;
        int attempts = 0;

        while (attempts++ < maxAttempts)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                for (int y = 0; y < mapSize.y; y++)
                {
                    possibleRooms[x, y] = new List<DungeonRoom>(roomPrefabs);

                    possibleRooms[0, y] = new List<DungeonRoom> { darkRoom };
                    possibleRooms[x, 0] = new List<DungeonRoom> { darkRoom };
                    possibleRooms[mapSize.x - 1, y] = new List<DungeonRoom> { darkRoom };
                    possibleRooms[x, mapSize.y - 1] = new List<DungeonRoom> { darkRoom };
                }
            }

            possibleRooms[1, 1] = new List<DungeonRoom> { startRoom };
            possibleRooms[mapSize.x - 2, mapSize.y - 2] = new List<DungeonRoom> { endRoom };


            recalculatePossibleRoomsQueue.Clear();
            EnqueNeighborToRecalculate(new Vector2Int(1, 1));

            bool success = GenerateAllPossibleTiles();
            if (success) break;
        }

        PlaceAllTiles();
    }

    private void ReGenerate(List<DungeonRoom> roomsToRegenerate)
    {
        int maxAttempts = 10;
        int attempts = 0;
        List<bool>  successes = new List<bool>();
        while (attempts++ < maxAttempts)
        {
            recalculatePossibleRoomsQueue.Clear();
            foreach (var room in roomsToRegenerate)
            {
                Vector2Int position = new Vector2Int(room.gridPosition.x, room.gridPosition.y);
                List<DungeonRoom> possibleRoomsHere = possibleRooms[position.x, position.y];
                int countRemove = possibleRoomsHere.RemoveAll(t => IsTilePossible(t, position));
                
                if (countRemove > 0) EnqueNeighborToRecalculate(position);
                
                possibleRoomsHere.AddRange(roomPrefabs);
                
                var PlusborderCondition = position.x + 1 == 0 || position.y + 1 == 0 ||
                                      position.x + 1 == mapSize.x - 1 || position.y + 1 == mapSize.y - 1;
                
                var MinusBorderCondition = position.x - 1 == 0 || position.y - 1 == 0 ||
                                           position.x - 1 == mapSize.x - 1 || position.y - 1 == mapSize.y - 1;
                
                var PlusstartTileCondition = position.x + 1 == 1 || position.y + 1 == 1;
                var MinusStartCondition = position.x - 1 == 1 || position.y - 1 == 1;
                
                var PlusendTileCondition = position.x + 1 == mapSize.x - 2 || position.y + 1 == mapSize.y - 2;
                var MinusendTileCondition = position.x - 1 == mapSize.x - 2 || position.y - 1 == mapSize.y - 2;
                
                if (PlusborderCondition || MinusBorderCondition || PlusstartTileCondition || MinusStartCondition || PlusendTileCondition || MinusendTileCondition)
                {
                    
                }
                else
                {
                    possibleRooms[position.x + 1, position.y] = new List<DungeonRoom>(roomPrefabs);
                    possibleRooms[position.x - 1, position.y] = new List<DungeonRoom>(roomPrefabs);
                    possibleRooms[position.x, position.y + 1] = new List<DungeonRoom>(roomPrefabs);
                    possibleRooms[position.x, position.y - 1] = new List<DungeonRoom>(roomPrefabs);
                }


                EnqueNeighborToRecalculate(position);
                
                successes.Add(GenerateAllPossibleTiles());
            }

            foreach (var success in successes)
            {
                if (!success)
                {
                    Debug.Log("No Succes");
                }
            }
            break;
        }

        RePlaceAllTiles(roomsToRegenerate);
    }



    private bool GenerateAllPossibleTiles()
    {
        int maxIterations = mapSize.x * mapSize.y;
        int iterations = 0;
        int backtrack = 0;

        while (iterations++ < maxIterations)
        {
            int maxInnerIterations = 500;
            int innerIterations = 0;

            while (recalculatePossibleRoomsQueue.Count > 0 && innerIterations++ < maxInnerIterations)
            {
                Vector2Int position = recalculatePossibleRoomsQueue.Dequeue();

                var borderCondition = position.x == 0 || position.y == 0 ||
                                      position.x == mapSize.x - 1 || position.y == mapSize.y - 1;
                var startTileCondition = position == new Vector2Int(1, 1);
                var endTileCondition = position == new Vector2Int(mapSize.x - 2, mapSize.y - 2);
                if (borderCondition || startTileCondition || endTileCondition)
                {
                    continue;
                }

                List<DungeonRoom> possibleRoomsHere = possibleRooms[position.x, position.y];
                int countRemove = possibleRoomsHere.RemoveAll(t => !IsTilePossible(t, position));

                if (countRemove > 0) EnqueNeighborToRecalculate(position);

                if (possibleRoomsHere.Count == 0)
                {
                    // possibleRooms[position.x, position.y] = new List<DungeonRoom> { darkRoom };

                    possibleRoomsHere.AddRange(roomPrefabs);
                    possibleRooms[position.x + 1, position.y] = new List<DungeonRoom>(roomPrefabs);
                    possibleRooms[position.x - 1, position.y] = new List<DungeonRoom>(roomPrefabs);
                    possibleRooms[position.x, position.y + 1] = new List<DungeonRoom>(roomPrefabs);
                    possibleRooms[position.x, position.y - 1] = new List<DungeonRoom>(roomPrefabs);

                    EnqueNeighborToRecalculate(position);
                    backtrack++;
                }
            }

            if (innerIterations == maxIterations) break;

            List<DungeonRoom> maxCountTile = possibleRooms[1, 1];
            Vector2Int maxTileCountPosition = new Vector2Int(1, 1);
            for (int x = 1; x < mapSize.x - 1; x++)
            {
                for (int y = 1; y < mapSize.y - 1; y++)
                {
                    if (possibleRooms[x, y] == possibleRooms[1, 1]) continue;
                    if (possibleRooms[x, y] == possibleRooms[mapSize.x - 2, mapSize.y - 2]) continue;

                    if (possibleRooms[x, y].Count > maxCountTile.Count)
                    {
                        maxCountTile = possibleRooms[x, y];
                        maxTileCountPosition = new Vector2Int(x, y);
                    }
                }
            }

            if (maxCountTile.Count == 1)
            {
                //Debug.Log($"Generate for:  {iterations} iterations with {backtrack} backtracks");
                return true;
            }

            DungeonRoom roomToCollapse = GetRandomRoom(maxCountTile);
            possibleRooms[maxTileCountPosition.x, maxTileCountPosition.y] = new List<DungeonRoom> { roomToCollapse };
            EnqueNeighborToRecalculate(maxTileCountPosition);
        }


        Debug.Log("Generation Failed");
        return false;
    }

    private bool IsTilePossible(DungeonRoom room, Vector2Int position)
    {
        bool isAllRightImpossible = possibleRooms[position.x + 1, position.y]
            .All(rightTile => !CanConnectRooms(room, rightTile, Direction.Right));
        if (isAllRightImpossible) return false;

        bool isAllLeftImpossible = possibleRooms[position.x - 1, position.y]
            .All(leftTile => !CanConnectRooms(room, leftTile, Direction.Left));
        if (isAllLeftImpossible) return false;

        bool isAllForwardImpossible = possibleRooms[position.x, position.y + 1]
            .All(fwdTile => !CanConnectRooms(room, fwdTile, Direction.Forward));
        if (isAllForwardImpossible) return false;

        bool isAllBackImpossible = possibleRooms[position.x, position.y - 1]
            .All(backTile => !CanConnectRooms(room, backTile, Direction.Backward));
        if (isAllBackImpossible) return false;

        return true;
    }

    private void PlaceAllTiles()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                PlaceTile(x, y);
            }
        }
    }

    private void RePlaceAllTiles(List<DungeonRoom> roomsToReplace)
    {
        foreach (var room in roomsToReplace)
        {
            Vector2Int position = new Vector2Int(room.gridPosition.x, room.gridPosition.y);
            Destroy(room.gameObject);
            ReplaceTile(position.x, position.y);
        }
    }

    private void EnqueNeighborToRecalculate(Vector2Int position)
    {
        recalculatePossibleRoomsQueue.Enqueue(new Vector2Int(position.x + 1, position.y));
        recalculatePossibleRoomsQueue.Enqueue(new Vector2Int(position.x - 1, position.y));
        recalculatePossibleRoomsQueue.Enqueue(new Vector2Int(position.x, position.y + 1));
        recalculatePossibleRoomsQueue.Enqueue(new Vector2Int(position.x, position.y - 1));
    }

    private void PlaceTile(int x, int y)
    {
        List<DungeonRoom> availableRooms = possibleRooms[x, y];

        if (availableRooms.Count == 0) return;

        DungeonRoom selectedRoom = GetRandomRoom(availableRooms);
        if (selectedRoom == null)
        {
            throw new ArgumentException($"Selected room: {selectedRoom.name} is null");
        }

        Vector3 size = selectedRoom.transform.GetChild(0).GetComponent<Renderer>().bounds.size;
        if (cellSize == Vector3.zero)
        {
            cellSize = size;
        }

        var angle = selectedRoom.transform.rotation.eulerAngles;
        var rotate = Quaternion.Euler(angle.x, angle.y, angle.z);
        spawnedRooms[x, y] = Instantiate(selectedRoom, new Vector3(x * size.x, 0, y * size.z), rotate);
        spawnedRooms[x, y].name = $"{x} - {y}";
        spawnedRooms[x, y].gridPosition = new Vector2Int(x, y);
    }

    private void ReplaceTile(int x, int y)
    {
        List<DungeonRoom> availableRooms = possibleRooms[x, y];
        if (availableRooms.Count == 0) return;

        DungeonRoom selectedRoom = GetRandomRoom(availableRooms);
        if (selectedRoom == null)
        {
            throw new ArgumentException($"Selected room: {selectedRoom.name} is null");
        }

        Vector3 size = selectedRoom.transform.GetChild(0).GetComponent<Renderer>().bounds.size;
        if (cellSize == Vector3.zero)
        {
            cellSize = size;
        }

        var angle = selectedRoom.transform.rotation.eulerAngles;
        var rotate = Quaternion.Euler(angle.x, angle.y, angle.z);
        spawnedRooms[x, y] = Instantiate(selectedRoom, new Vector3(x * size.x, 0, y * size.z), rotate);
        spawnedRooms[x, y].name = $"{x} - {y}";
        spawnedRooms[x, y].gridPosition = new Vector2Int(x, y);
    }


    private DungeonRoom GetRandomRoom(List<DungeonRoom> availableRooms)
    {
        List<float> chances = new List<float>();
        for (int i = 0; i < availableRooms.Count; i++)
        {
            chances.Add(availableRooms[i].Weight);
        }

        float value = Random.Range(0, chances.Sum());
        float sum = 0;

        for (int i = 0; i < chances.Count; i++)
        {
            sum += chances[i];
            if (value < sum)
            {
                return availableRooms[i];
            }
        }

        return availableRooms[availableRooms.Count - 1];
    }


    private bool CanConnectRooms(DungeonRoom existingRoom, DungeonRoom roomToConnect, Direction direction)
    {
        if (existingRoom == null) return true;


        var existingRoomDirection = GetDirection(existingRoom, direction);
        var roomToConnectRverceDirection = GetDirection(roomToConnect, ReverseDirection(direction));

        var directionTypeCondition =
            Enumerable.SequenceEqual(existingRoomDirection.DirectionTypes, roomToConnectRverceDirection.DirectionTypes);

        return directionTypeCondition;
    }

    private DungeonRoom.RoomDirection GetDirection(DungeonRoom room, Direction direction)
    {
        foreach (var dir in room.Directions)
        {
            if (dir.Direction == direction)
            {
                return dir;
            }
        }

        throw new ArgumentException("No Direction in: " + room + " With the direction: " + direction);
    }

    private Direction ReverseDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Forward:
            {
                return Direction.Backward;
            }
            case Direction.Backward:
            {
                return Direction.Forward;
            }
            case Direction.Left:
            {
                return Direction.Right;
            }
            case Direction.Right:
            {
                return Direction.Left;
            }
            default:
            {
                throw new ArgumentException("Wrong ReverseDirection: " + dir);
            }
        }
    }
}