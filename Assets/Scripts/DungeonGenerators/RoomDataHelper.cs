using System;
using System.Collections.Generic;
using UnityEngine;

public static class RoomDataHelper
{
    public static RoomDirection GetDirection(DungeonRoom room, Direction direction)
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
    // public static Vector2Int FindRoomWithMaximumCount(Vector2Int mapSize, List<DungeonRoom>[,] possibleRooms)
    // {
    //     Vector2Int maxTileCountPosition = new Vector2Int(1, 1);
    //     List<DungeonRoom> maxCountTile = possibleRooms[1, 1];
    //
    //     for (int x = 1; x < mapSize.x - 1; x++)
    //     {
    //         for (int y = 1; y < mapSize.y - 1; y++)
    //         {
    //             if (RoomConditions.IsStartOrEndRoom(new Vector2Int(x, y), mapSize)) continue;
    //
    //             if (possibleRooms[x, y].Count > maxCountTile.Count)
    //             {
    //                 maxCountTile = possibleRooms[x, y];
    //                 maxTileCountPosition = new Vector2Int(x, y);
    //             }
    //         }
    //     }
    //
    //     return maxTileCountPosition;
    // }
    
    public static Vector2Int FindRoomWithMaximumCount(Vector2Int mapSize, List<DungeonRoom>[,] possibleRooms, Vector2Int mapStart)
    {
        Vector2Int maxTileCountPosition = new Vector2Int(1, 1);
        List<DungeonRoom> maxCountTile = null;

        for (int x = 1; x < mapSize.x - 1; x++)
        {
            for (int y = 1; y < mapSize.y - 1; y++)
            {
                if (RoomConditions.IsStartOrEndRoom(new Vector2Int(x, y), mapSize, mapStart)) continue;

                List<DungeonRoom> currentTileRooms = possibleRooms[x, y];
            
                if (currentTileRooms != null && currentTileRooms.Count > 0)
                {
                    if (maxCountTile == null || currentTileRooms.Count > maxCountTile.Count)
                    {
                        maxCountTile = currentTileRooms;
                        maxTileCountPosition = new Vector2Int(x, y);
                    }
                }
            }
        }

        return maxTileCountPosition;
    }
    public static Direction ReverseDirection(Direction dir)
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