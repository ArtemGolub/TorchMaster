using System.Collections.Generic;
using UnityEngine;

public sealed class MonoTile : MonoBehaviour
{
    [SerializeField] private TileSO tilePreset;
    
    public List<Transform> itemSpawnPoints;
    [SerializeField] private List<Transform> TileSpawnPoints = new List<Transform>();
    public Dictionary<Transform, bool> nextTileSpawnPoints = new Dictionary<Transform, bool>();
    
    
    private Tile _tile;

    private void Awake()
    {
        CreateTile();
        
        ItemFabric.current.SpawnItems(_tile, itemSpawnPoints);
        TileFabric.current.GenerateNextTile(_tile, nextTileSpawnPoints);
    }
    
    void CreateTile()
    {
        nextTileSpawnPoints = new Dictionary<Transform, bool>();
        foreach (var point in TileSpawnPoints)
        {
            nextTileSpawnPoints.Add(point, false);
        }
        _tile = TileGenerator.current.CreateTile(tilePreset);
    }
    
}