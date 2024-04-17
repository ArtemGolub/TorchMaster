using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public List<DungeonRoom> roomPrefabs;
    public Vector2Int mapStart;
    public Vector2Int mapSize;
    private DungeonRoom[,] spawnedRooms;

    private Queue<Vector2Int> recalculatePossibleRoomsQueue = new Queue<Vector2Int>();
    private List<DungeonRoom>[,] possibleRooms;

    public DungeonRoom startRoom;
    public DungeonRoom endRoom;
    public DungeonRoom darkRoom;
    public DungeonRoom XRoom;
    public DungeonRoom Troom;

    private RoomCreator _roomCreator;
    private DungeonRegenerator _dungeonRegenerator;

    private void Start()
    {
        _roomCreator = GetComponent<RoomCreator>();
        _roomCreator.SetRoomPrefabs(roomPrefabs);
        _roomCreator.CreateRotatedRooms();
        _roomCreator.CreateRotatedRoomsWay();

        _dungeonRegenerator = GetComponent<DungeonRegenerator>();

    
        mapStart = new Vector2Int(7, 7);
        spawnedRooms = new DungeonRoom[mapStart.x + mapSize.x, mapStart.y + mapSize.y];
        
        // InitDefaultRooms();
        //_wayCreator.CheckDistanceFromStart();
        GenerateDungeon();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _dungeonRegenerator.RegenerateDungeon(spawnedRooms);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GerenerateNext();
        }
    }

    public void GerenerateNext()
    {
        _dungeonRegenerator.RemoveDungeon(spawnedRooms);
        mapStart = new Vector2Int(mapSize.x - 2, mapSize.y - 1);
        mapSize += new Vector2Int(7, 7);
        spawnedRooms = new DungeonRoom[mapSize.x, mapSize.y];
        GenerateDungeon();
    }

    public void GenerateDungeon()
    {
        possibleRooms = new List<DungeonRoom>[mapSize.x, mapSize.y];

        int maxAttempts = 10;
        int attempts = 0;

        while (attempts++ < maxAttempts)
        {
            for (int x = mapStart.x; x < mapSize.x; x++)
            {
                for (int y = mapStart.y; y < mapSize.y; y++)
                {
                    possibleRooms[x, y] = new List<DungeonRoom>(roomPrefabs);

                    possibleRooms[mapStart.x, y] = new List<DungeonRoom> { darkRoom };
                    possibleRooms[x, mapStart.y] = new List<DungeonRoom> { darkRoom };
                    possibleRooms[mapStart.x + mapSize.x - 1, y] = new List<DungeonRoom> { darkRoom };
                    possibleRooms[x, mapStart.y + mapSize.y - 1] = new List<DungeonRoom> { darkRoom };
                }
            }

            possibleRooms[mapStart.x, mapStart.y] = new List<DungeonRoom> { startRoom };
            possibleRooms[mapStart.x + 1, mapStart.y + 1] = new List<DungeonRoom> { XRoom };
            possibleRooms[mapStart.x + 2, mapStart.y + 2] = new List<DungeonRoom> { XRoom };
            
            possibleRooms[mapStart.x + mapSize.x - 2, mapStart.y + mapSize.y - 2] = new List<DungeonRoom> { endRoom };
            possibleRooms[mapStart.x + mapSize.x - 3, mapStart.y + mapSize.y - 3] = new List<DungeonRoom> { XRoom };
   
             possibleRooms[mapStart.x + mapSize.x - 2,  mapStart.y + mapSize.y - 1] = new List<DungeonRoom> { startRoom };
             possibleRooms[mapStart.x + mapSize.x - 1,  mapStart.y + mapSize.y - 1] = new List<DungeonRoom> { Troom };
             
             possibleRooms[(mapStart.x + mapSize.x) /2, (mapStart.y + mapSize.y) /2] = new List<DungeonRoom> { XRoom };
             
             possibleRooms[mapStart.x + 1, mapStart.y] = new List<DungeonRoom> {  Troom};
             possibleRooms[mapStart.x, mapStart.y + 1] = new List<DungeonRoom> { startRoom };

            recalculatePossibleRoomsQueue.Clear();
            EnqueNeighborToRecalculate(new Vector2Int(mapStart.x + 1, mapStart.y + 1));

            bool success = GenerateAllPossibleTiles();
            if (success) break;
        }

        _roomCreator.PlaceAllTiles(mapSize, possibleRooms, spawnedRooms);
    }

    private bool GenerateAllPossibleTiles()
    {
        int maxIterations = mapSize.x * mapSize.y;
        int iterations = 0;
        int backtrack = 0;

        while (iterations++ < maxIterations)
        {
            const int maxInnerIterations = 500;
            int innerIterations = 0;

            while (recalculatePossibleRoomsQueue.Count > 0 && innerIterations++ < maxInnerIterations)
            {
                Vector2Int position = recalculatePossibleRoomsQueue.Dequeue();

                if (RoomConditions.IsBorderTile(position, mapSize, mapStart) ||
                    RoomConditions.IsStartOrEndRoom(position, mapSize, mapStart) ||
                    RoomConditions.IsMainPath(spawnedRooms, position))
                {
                    continue;
                }

                List<DungeonRoom> possibleRoomsHere = possibleRooms[position.x, position.y];
                Debug.Log(position);
                int countRemove = RoomConditions.RemoveImpossibleRooms(possibleRoomsHere, position, possibleRooms);

                if (countRemove > 0) EnqueNeighborToRecalculate(position);

                if (possibleRoomsHere.Count == 0)
                {
                    ResetPossibleRooms(position);
                    EnqueNeighborToRecalculate(position);
                    backtrack++;
                }
            }

            if (innerIterations == maxIterations) break;

            Vector2Int maxTileCountPosition = RoomDataHelper.FindRoomWithMaximumCount(mapSize, possibleRooms, mapStart);
            List<DungeonRoom> maxCountTile = possibleRooms[maxTileCountPosition.x, maxTileCountPosition.y];
            if (maxCountTile.Count == 1)
            {
                return true;
            }

            CollapseRoom(maxTileCountPosition, maxCountTile);
        }

        return false;
    }

    private void EnqueNeighborToRecalculate(Vector2Int position)
    {
        recalculatePossibleRoomsQueue.Enqueue(new Vector2Int(position.x + 1, position.y));
        recalculatePossibleRoomsQueue.Enqueue(new Vector2Int(position.x - 1, position.y));
        recalculatePossibleRoomsQueue.Enqueue(new Vector2Int(position.x, position.y + 1));
        recalculatePossibleRoomsQueue.Enqueue(new Vector2Int(position.x, position.y - 1));
    }

    private void ResetPossibleRooms(Vector2Int position)
    {
        possibleRooms[position.x, position.y] = new List<DungeonRoom>(roomPrefabs);
        possibleRooms[position.x + 1, position.y] = new List<DungeonRoom>(roomPrefabs);
        possibleRooms[position.x - 1, position.y] = new List<DungeonRoom>(roomPrefabs);
        possibleRooms[position.x, position.y + 1] = new List<DungeonRoom>(roomPrefabs);
        possibleRooms[position.x, position.y - 1] = new List<DungeonRoom>(roomPrefabs);
    }

    private void CollapseRoom(Vector2Int position, List<DungeonRoom> maxCountTile)
    {
        DungeonRoom roomToCollapse = RoomRandomizer.GetRandomRoom(maxCountTile);
        possibleRooms[position.x, position.y] = new List<DungeonRoom> { roomToCollapse };
        EnqueNeighborToRecalculate(position);
    }
}