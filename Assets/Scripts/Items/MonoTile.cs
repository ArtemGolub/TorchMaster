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
    
    [SerializeField] private TileSO tilePreset;

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
        _tile = TileGenerator.current.CreateTile(tilePreset);
        //ItemFabric.current.SpawnItems(_tile, itemSpawnPoints);
        //CharacterFabric.current.TileSpawnCharacter(_tile, enemySpawnPoints, patrolPoints);
    }
    
    public Directions GetDoorByDirection(Direction direction)
    {
        for (int i = 0; i < directions.Count; i++)
        {
            if (directions[i].direction == direction)
            {
                return directions[i];
            }
        }

        return null;
    }

    public Direction ReverseDirection(Direction direction)
    {
        switch (direction)
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
                Debug.LogError("No reverse direction for: " + direction);
                return Direction.Forward;
            }
        }
    }
    
}