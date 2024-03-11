using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public sealed class MonoTile : MonoBehaviour
{
    [Serializable]
    public class Directions
    {
        [FormerlySerializedAs("Direction")] public Orientation orientation;
        [FormerlySerializedAs("type")] [FormerlySerializedAs("DirectionType")] public DirectionType directionType;
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
    
    public Directions GetDoorByDirection(Orientation orientation)
    {
        for (int i = 0; i < directions.Count; i++)
        {
            if (directions[i].orientation == orientation)
            {
                return directions[i];
            }
        }

        return null;
    }

    public Orientation ReverseDirection(Orientation orientation)
    {
        switch (orientation)
        {
            case Orientation.Forward:
            {
                return Orientation.Backward;
            }
            case Orientation.Backward:
            {
                return Orientation.Forward;
            }
            case Orientation.Left:
            {
                return Orientation.Right;
            }
            case Orientation.Right:
            {
                return Orientation.Left;
            }
            default:
            {
                Debug.LogError("No reverse direction for: " + orientation);
                return Orientation.Forward;
            }
        }
    }
    
}