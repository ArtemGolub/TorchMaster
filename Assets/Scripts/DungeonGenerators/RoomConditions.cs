using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class RoomConditions
{
    public static bool IsTilePossible(DungeonRoom room, Vector2Int position, List<DungeonRoom>[,] possibleRooms)
    {
        int gridSizeX = possibleRooms.GetLength(0);
        int gridSizeY = possibleRooms.GetLength(1);
        
        if (position.x + 1 < gridSizeX && possibleRooms[position.x + 1, position.y] != null)
        {
            bool isAllRightImpossible = possibleRooms[position.x + 1, position.y]
                .All(rightTile => rightTile != null && !CanConnectRooms(room, rightTile, Direction.Right));
            if (isAllRightImpossible) return false;
        }
        else
        {
            return false; // Если соседний тайл справа не существует или равен null, возвращаем false
        }

        if (position.x - 1 >= 0 && possibleRooms[position.x - 1, position.y] != null)
        {
            bool isAllLeftImpossible = possibleRooms[position.x - 1, position.y]
                .All(leftTile => leftTile != null && !CanConnectRooms(room, leftTile, Direction.Left));
            if (isAllLeftImpossible) return false;
        }
        else
        {
            return false;  // Если соседний тайл слева не существует или равен null, возвращаем false
        }

        if (position.y + 1 < gridSizeY && possibleRooms[position.x, position.y + 1] != null)
        {
            bool isAllForwardImpossible = possibleRooms[position.x, position.y + 1]
                .All(fwdTile => fwdTile != null && !CanConnectRooms(room, fwdTile, Direction.Forward));
            if (isAllForwardImpossible) return false;
        }
        else
        {
            return false; // Если соседний тайл спереди не существует или равен null, возвращаем false
        }

        if (position.y - 1 >= 0 && possibleRooms[position.x, position.y - 1] != null)
        {
            bool isAllBackImpossible = possibleRooms[position.x, position.y - 1]
                .All(backTile => backTile != null && !CanConnectRooms(room, backTile, Direction.Backward));
            if (isAllBackImpossible) return false;
        }
        else
        {
            return false;  // Если соседний тайл сзади не существует или равен null, возвращаем false
        }

        return true;
    }

    private static bool CanConnectRooms(DungeonRoom existingRoom, DungeonRoom roomToConnect, Direction direction)
    {
        if (existingRoom == null) return true;


        var existingRoomDirection = RoomDataHelper.GetDirection(existingRoom, direction);
        var roomToConnectRverceDirection =
            RoomDataHelper.GetDirection(roomToConnect, RoomDataHelper.ReverseDirection(direction));


        for (int i = 0; i < existingRoomDirection.DirectionTypes.Length; i++)
        {
            var darkWallCondition = existingRoomDirection.DirectionTypes[i] == DirectionType.Shadow ||
                                    existingRoomDirection.DirectionTypes[i] == DirectionType.Wall ||
                                    existingRoomDirection.DirectionTypes[i] == DirectionType.Empty;
            var wallDarkCondition = roomToConnectRverceDirection.DirectionTypes[i] == DirectionType.Shadow ||
                                    roomToConnectRverceDirection.DirectionTypes[i] == DirectionType.Wall ||
                                    roomToConnectRverceDirection.DirectionTypes[i] == DirectionType.Empty;
            var riverCondition = existingRoomDirection.DirectionTypes[i] == DirectionType.River ||
                                 existingRoomDirection.DirectionTypes[i] == DirectionType.Shadow;
            var conditionRiver = roomToConnectRverceDirection.DirectionTypes[i] == DirectionType.River ||
                                 roomToConnectRverceDirection.DirectionTypes[i] == DirectionType.Shadow;


            if (existingRoomDirection.DirectionTypes[i] == roomToConnectRverceDirection.DirectionTypes[i]) continue;
            if (darkWallCondition && wallDarkCondition) continue;
            if (riverCondition && conditionRiver) continue;

            return false;
        }
        return true;
    }

    public static bool IsBorderTile(Vector2Int position, Vector2Int mapSize, Vector2Int mapStart)
    {
        var startBorderX = position.x == mapStart.x;
        var startBorderY = position.y == mapStart.y;
        var endBorderX = position.x == mapStart.x + mapSize.x - 1;
        var endBorderY = position.y == mapStart.y + mapSize.y - 1;
        
        return  startBorderX || startBorderY  ||endBorderX ||endBorderY;
    }

    public static bool IsStartOrEndRoom(Vector2Int position, Vector2Int mapSize, Vector2Int mapStart)
    {
        return position == new Vector2Int(
                mapStart.x + 1, mapStart.y + 1) 
               || position == new Vector2Int(mapStart.x + mapSize.x - 2, mapStart.y + mapSize.y - 2) 
               || position == new Vector2Int(mapStart.x + 2,mapStart.y + 2) 
               || position == new Vector2Int(mapStart.x + mapSize.x - 3, mapStart.y + mapSize.y - 3);
    }

    public static int RemoveImpossibleRooms(List<DungeonRoom> possibleRoomsHere, Vector2Int position,
        List<DungeonRoom>[,] possibleRooms)
    {
        return possibleRoomsHere.RemoveAll(tile => !IsTilePossible(tile, position, possibleRooms));
    }

    public static bool IsMainPath(DungeonRoom[,] spawnedRooms,Vector2Int position)
    {
        var room = spawnedRooms[position.x, position.y];
        if (room == null) return false;
        return room.MainPath;
    }
}