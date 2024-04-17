using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonRegenerator : MonoBehaviour
{
    private DungeonGenerator _dungeonGenerator;
    private Grid _pathFinderGrid;
    private PathFinder _pathFinder;
    private RoomCreator _roomCreator;
    private Vector3 cellSize;

    private List<DungeonRoom>[,] possibleRooms;
    private Queue<Vector2Int> recalculatePossibleRoomsQueue = new Queue<Vector2Int>();

    private void Start()
    {
        _dungeonGenerator = transform.GetComponent<DungeonGenerator>();
        _pathFinderGrid = transform.GetComponent<Grid>();
        _pathFinder = transform.GetComponent<PathFinder>();
        _roomCreator = transform.GetComponent<RoomCreator>();
    }

    public void RegenerateDungeon(DungeonRoom[,] spawnedRooms)
    {
        foreach (DungeonRoom room in spawnedRooms)
        {
            if (room != null) Destroy(room.gameObject);
        }

        _dungeonGenerator.GenerateDungeon();
    }

    public void RemoveDungeon(DungeonRoom[,] spawnedRooms)
    {
        foreach (DungeonRoom room in spawnedRooms)
        {
            if (room != null) Destroy(room.gameObject);
        }
    }

}