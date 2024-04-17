using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreator : MonoBehaviour
{
    private List<DungeonRoom> _roomPrefabs;
    public List<DungeonRoom> _wayPrefabs;
    private Transform rotatedRoomContainer;
    private Transform roomContainer;

    public void SetRoomPrefabs(List<DungeonRoom> roomPrefabs)
    {
        _roomPrefabs = roomPrefabs;
    }
    public void CreateRotatedRooms()
    {
        CreateRotatedRoomContainer();
        CreateRoomContainer();
        
        var countBeforeAdding = _roomPrefabs.Count;
        DungeonRoom clone;
        for (int i = 0; i < countBeforeAdding; i++)
        {
            switch (_roomPrefabs[i].Rotation)
            {
                case DungeonRoom.RotationType.OnlyRotation:
                    break;
                case DungeonRoom.RotationType.TwoRotations:
                    clone = Instantiate(_roomPrefabs[i], transform.position + Vector3.right * -200, Quaternion.identity, rotatedRoomContainer);
                    clone.RotateRoom90();
                    clone.transform.name += " +90";
                    clone.Weight /= 2;
                    if (clone.Weight <= 0) clone.Weight = 1;
                    
                    _roomPrefabs.Add(clone);
                    break;
                case DungeonRoom.RotationType.FourRotations:

                    clone = Instantiate(_roomPrefabs[i], transform.position + Vector3.right * -200, Quaternion.identity, rotatedRoomContainer);
                    clone.RotateRoom90();
                    clone.transform.name += " +90";
                    clone.Weight /= 4;                                   
                    if (clone.Weight <= 0) clone.Weight = 1;         
                    _roomPrefabs.Add(clone);

                    clone = Instantiate(_roomPrefabs[i], transform.position + Vector3.right * -200, Quaternion.identity, rotatedRoomContainer);
                    clone.RotateRoom90();
                    clone.RotateRoom90();
                    clone.transform.name += " +180";
                    clone.Weight /= 4;                                   
                    if (clone.Weight <= 0) clone.Weight = 1;         
                    _roomPrefabs.Add(clone);

                    clone = Instantiate(_roomPrefabs[i], transform.position + Vector3.right * -200, Quaternion.identity,rotatedRoomContainer);
                    clone.RotateRoom90();
                    clone.RotateRoom90();
                    clone.RotateRoom90();
                    clone.transform.name += " -90";
                    clone.Weight /= 4;                                   
                    if (clone.Weight <= 0) clone.Weight = 1;         
                    _roomPrefabs.Add(clone);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    
    public void CreateRotatedRoomsWay()
    {
        CreateRotatedRoomContainer();
        
        var countBeforeAdding = _wayPrefabs.Count;
        DungeonRoom clone;
        for (int i = 0; i < countBeforeAdding; i++)
        {
            switch (_wayPrefabs[i].Rotation)
            {
                case DungeonRoom.RotationType.OnlyRotation:
                    break;
                case DungeonRoom.RotationType.TwoRotations:
                    clone = Instantiate(_wayPrefabs[i], transform.position + Vector3.right * -200, Quaternion.identity, rotatedRoomContainer);
                    clone.RotateRoom90();
                    clone.transform.name += " +90";
                    clone.Weight /= 2;
                    if (clone.Weight <= 0) clone.Weight = 1;
                    
                    _wayPrefabs.Add(clone);
                    break;
                case DungeonRoom.RotationType.FourRotations:

                    clone = Instantiate(_wayPrefabs[i], transform.position + Vector3.right * -200, Quaternion.identity, rotatedRoomContainer);
                    clone.RotateRoom90();
                    clone.transform.name += " +90";
                    clone.Weight /= 4;                                   
                    if (clone.Weight <= 0) clone.Weight = 1;         
                    _wayPrefabs.Add(clone);

                    clone = Instantiate(_wayPrefabs[i], transform.position + Vector3.right * -200, Quaternion.identity, rotatedRoomContainer);
                    clone.RotateRoom90();
                    clone.RotateRoom90();
                    clone.transform.name += " +180";
                    clone.Weight /= 4;                                   
                    if (clone.Weight <= 0) clone.Weight = 1;         
                    _wayPrefabs.Add(clone);

                    clone = Instantiate(_wayPrefabs[i], transform.position + Vector3.right * -200, Quaternion.identity,rotatedRoomContainer);
                    clone.RotateRoom90();
                    clone.RotateRoom90();
                    clone.RotateRoom90();
                    clone.transform.name += " -90";
                    clone.Weight /= 4;                                   
                    if (clone.Weight <= 0) clone.Weight = 1;         
                    _wayPrefabs.Add(clone);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void CreateRotatedRoomContainer()
    {
        rotatedRoomContainer = new GameObject().transform;
        rotatedRoomContainer.name = "RotatedRoomContainer";
        rotatedRoomContainer.SetParent(transform);
    }
    
    public void PlaceAllTiles(Vector2Int mapSize, List<DungeonRoom>[,] possibleRooms, DungeonRoom[,] spawnedRooms)
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                PlaceTile(x, y, possibleRooms, spawnedRooms);
            }
        }
    }

    private void CreateRoomContainer()
    {
        roomContainer = new GameObject().transform;
        roomContainer.name = "RoomContainer";
        roomContainer.SetParent(transform);
    }
    
    public void PlaceTile(int x, int y, List<DungeonRoom>[,] possibleRooms, DungeonRoom[,] spawnedRooms)
    {
        List<DungeonRoom> availableRooms = possibleRooms[x, y];

        if (availableRooms.Count == 0) return;

        DungeonRoom selectedRoom = RoomRandomizer.GetRandomRoom(availableRooms);
        if (selectedRoom == null)
        {
            throw new ArgumentException($"Selected room: {selectedRoom.name} is null");
        }

        Vector3 size = selectedRoom.transform.GetChild(0).GetComponent<Renderer>().bounds.size;
        
        var angle = selectedRoom.transform.rotation.eulerAngles;
        var rotate = Quaternion.Euler(angle.x, angle.y, angle.z);
        spawnedRooms[x, y] = Instantiate(selectedRoom, new Vector3(x * size.x, 0, y * size.z), rotate, roomContainer);
        spawnedRooms[x, y].name += $" {x} - {y}";
        spawnedRooms[x, y].gridPosition = new Vector2Int(x, y);
    }
    
    
    public void RePlaceTile(int x, int y, List<DungeonRoom>[,] possibleRooms, DungeonRoom[,] spawnedRooms)
    {
        List<DungeonRoom> availableRooms = possibleRooms[x, y];
        if (availableRooms.Count == 0) return;
      
        
        DungeonRoom selectedRoom = RoomRandomizer.GetRandomRoom(availableRooms);
        if (selectedRoom == null)
        {
            throw new ArgumentException($"Selected room: {selectedRoom.name} is null");
        }

        Vector3 size = selectedRoom.transform.GetChild(0).GetComponent<Renderer>().bounds.size;
        
        var angle = selectedRoom.transform.rotation.eulerAngles;
        var rotate = Quaternion.Euler(angle.x, angle.y, angle.z);
        
        Destroy(spawnedRooms[x,y]);
        spawnedRooms[x, y] = Instantiate(selectedRoom, new Vector3(x * size.x, 0, y * size.z), rotate, roomContainer);
        spawnedRooms[x, y].name += $" {x} - {y}";
        spawnedRooms[x, y].gridPosition = new Vector2Int(x, y);
    }
}