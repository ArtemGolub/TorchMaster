using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public sealed class MonoTile : MonoBehaviour
{
    [Serializable]
    public class Directions
    {
        [FormerlySerializedAs("orientation")] public Direction direction;
        public DirectionType directionType;
        public Transform transform;
        public Transform ConnectTo;
    }
    
    [FormerlySerializedAs("tilePreset")] [SerializeField] private RoomSO roomPreset;

    public List<Transform> itemSpawnPoints;
    [SerializeField] public List<Directions> directions = new List<Directions>();
    [SerializeField] private List<Transform> enemySpawnPoints = new List<Transform>();
    [SerializeField] private List<Transform> patrolPoints = new List<Transform>();

    public Tile _tile;

    public List<Directions> GetAllDirectionsByType(DirectionType directionType)
    {
        List<Directions> direction = new List<Directions>();
        foreach (var dir in directions)
        {
            if (dir.directionType == directionType)
            {
                direction.Add(dir);
            }
        }
        return direction;
    }
    private void Awake()
    {
        _tile = TileGenerator.current.CreateTile(roomPreset);
        //ItemFabric.current.SpawnItems(_tile, itemSpawnPoints);
        //CharacterFabric.current.TileSpawnCharacter(_tile, enemySpawnPoints, patrolPoints);
    }
    
    
    
}