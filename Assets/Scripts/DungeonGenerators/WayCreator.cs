// using System.Collections.Generic;
// using UnityEngine;
//
// public class WayCreator: MonoBehaviour
// {
//     public List<Vector2Int> path;
//     public List<DungeonRoom> pathPrefabs;
//     private Vector2Int startTile;
//     private Vector2Int MapSize;
//     
//     public void FindPath(DungeonRoom startRoom,DungeonRoom endRoom, Vector2Int mapSize)
//     {
//         
//         MapSize = mapSize;
//         path.Clear();
//         path.Add(new Vector2Int(endRoom.gridPosition.x, endRoom.gridPosition.y)); // Добавляем конечный тайл в начало пути
//
//         Vector2Int currentTile = new Vector2Int(endRoom.gridPosition.x, endRoom.gridPosition.y);
//         startTile = new Vector2Int(startRoom.gridPosition.x, startRoom.gridPosition.y);
//         while (currentTile != startTile)
//         {
//             // Получаем список соседних тайлов
//             List<Vector2Int> neighbors = GetNeighbors(currentTile);
//
//             // Выбираем следующий тайл из соседей, который приближает нас к старту
//             Vector2Int nextTile = ChooseNextTile(neighbors, currentTile);
//
//             // Если не удалось найти следующий тайл, выходим из цикла
//             if (nextTile == currentTile)
//                 break;
//
//             // Добавляем следующий тайл к пути
//             path.Add(nextTile);
//
//             // Переходим к следующему тайлу
//             currentTile = nextTile;
//         }
//     }
//     
//     private List<Vector2Int> GetNeighbors(Vector2Int tile)
//     {
//         List<Vector2Int> neighbors = new List<Vector2Int>();
//
//         // Добавляем соседние тайлы (вы можете настроить это под вашу сетку)
//         neighbors.Add(new Vector2Int(tile.x + 1, tile.y));
//         neighbors.Add(new Vector2Int(tile.x - 1, tile.y));
//         neighbors.Add(new Vector2Int(tile.x, tile.y + 1));
//         neighbors.Add(new Vector2Int(tile.x, tile.y - 1));
//
//         return neighbors;
//     }
//     
//     private Vector2Int ChooseNextTile(List<Vector2Int> neighbors, Vector2Int currentTile)
//     {
//         foreach (Vector2Int neighbor in neighbors)
//         {
//             // Проверяем, не достигли ли мы границы сетки
//             if (!RoomConditions.IsBorderTile(neighbor, MapSize) && !RoomConditions.IsStartOrEndRoom(neighbor, MapSize))
//             {
//                 if (CheckDistanceFromStart(neighbor) < CheckDistanceFromStart(currentTile))
//                 {
//                     return neighbor;
//                 }
//             }
//         }
//
//         // Если не удалось найти подходящий тайл, возвращаем текущий
//         return currentTile;
//     }
//     
//     private int CheckDistanceFromStart(Vector2Int tile)
//     {
//         // Ваша реализация этого метода
//         // Верните расстояние от этого тайла до стартового тайла
//         return Mathf.Abs(tile.x - startTile.x) + Mathf.Abs(tile.y - startTile.y);
//     }
//     
//     // public void CheckDistanceFromStart(DungeonRoom startRoom, DungeonRoom endRoom)
//     // {
//     //     var startPose = new Vector2Int(startRoom.gridPosition.x, startRoom.gridPosition.y);
//     //     var distanceFromStart = new Vector2Int(startRoom.gridPosition.x - endRoom.gridPosition.x,
//     //         startRoom.gridPosition.y - endRoom.gridPosition.y);
//     //     path.Add(distanceFromStart);
//     //
//     //     int maxAttempts = 10;
//     //     int attempts = 0;
//     //
//     //     while (attempts++ < maxAttempts)
//     //     {
//     //         while (path[path.Count - 1] != new Vector2Int(startRoom.gridPosition.x, startRoom.gridPosition.y))
//     //         {
//     //             GetRandomNeigbor();
//     //         } 
//     //     }
//     //     
//     // }
//     //
//     // private void GetRandomNeigbor()
//     // {
//     //     var chosenNeighborInt = new Vector2Int();
//     //     var lastAdded = path[path.Count - 1];
//     //     var randomize = Random.Range(0, 1);
//     //     if (randomize == 0)
//     //     {
//     //         chosenNeighborInt = new Vector2Int(lastAdded.x - 1, lastAdded.y);
//     //         chosenNeighborInt *= -1;
//     //         path.Add(chosenNeighborInt);
//     //     }
//     //     else
//     //     {
//     //         chosenNeighborInt = new Vector2Int(lastAdded.x, lastAdded.y - 1);
//     //         chosenNeighborInt *= -1;
//     //         path.Add(chosenNeighborInt);
//     //     }
//     //     Debug.Log(chosenNeighborInt);
//     // }
//     
// }