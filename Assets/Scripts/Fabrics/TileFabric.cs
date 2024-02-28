using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileFabric: MonoBehaviour
{
    public static TileFabric current;
    private Transform TileContainer;
    
    [SerializeField]private TileSO startTile;
    [SerializeField]private Transform spawnPose;
    
    private List<TileSO> _tileVariants;

    private int levelTiles = 0;
    private int maxTiles = 5;
    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        if (current != this)
        {
            Destroy(transform);
        }
    }
    private void Start()
    {
        CreateContainer();
        GenerateFirstTile(startTile, spawnPose);
    }

    private void CreateContainer()
    {
        GameObject containerObject = new GameObject("TileContainer");
        TileContainer = containerObject.transform;
    }

    private void GenerateFirstTile(TileSO startTile, Transform spawnPose)
    {
        var tile = Instantiate(startTile.prefab, spawnPose.position, spawnPose.rotation);
        tile.SetParent(TileContainer);
    }
    
    private void GetNextTilePose(Tile tile)
    {
        _tileVariants = tile.NextTileVariants;
    }
    
    public void GenerateNextTile(Tile tile, Dictionary<Transform, bool> nextTileSpawnPoints)
    {
        if (levelTiles >= maxTiles)
        {
            Debug.LogWarning("Maximum number of tiles reached. Unable to generate next tile.");
            return;
        }
        levelTiles += 1;
        if (tile == null)
        {
            Debug.LogError("Tile is null. Unable to generate next tile.");
            return;
        }
        
        if (nextTileSpawnPoints == null || nextTileSpawnPoints.Count == 0)
        {
            Debug.LogWarning("Next tile spawn points dictionary is null or empty.");
            return;
        }
        

        
        GetNextTilePose(tile);
        
        List<Transform> availableTiles = GetAvaliableTransforms(nextTileSpawnPoints);
       
        if (availableTiles == null || availableTiles.Count == 0)
        {
            Debug.LogWarning("No available tiles to generate next tile.");
            return;
        }
        
        Transform selectedTransform = GetRandomTileTransform(availableTiles);
       
        if (selectedTransform == null)
        {
            Debug.LogWarning("Selected transform for next tile is null.");
            return;
        }
        GameObject randomPrefab = GetRandomPrefab();
        var tileobj = Instantiate(randomPrefab, selectedTransform.position, selectedTransform.rotation);
        tileobj.transform.SetParent(TileContainer);
        Reset();
    }
    private List<Transform> GetAvaliableTransforms(Dictionary<Transform, bool> nextTileSpawnPoints)
    {
        if (nextTileSpawnPoints == null || nextTileSpawnPoints.Count == 0)
        {
            Debug.LogWarning("Next tile spawn points dictionary is null or empty.");
            return new List<Transform>();
        }

        List<Transform> availableTiles = new List<Transform>();
        foreach (var tilePose in nextTileSpawnPoints.Keys)
        {
            if (nextTileSpawnPoints[tilePose] == true) continue;
            availableTiles.Add(tilePose);
        }

        if (availableTiles.Count == 0)
        {
            Debug.LogWarning("No available tiles.");
            return new List<Transform>();
        }

        return availableTiles;
    }
    
    private Transform GetRandomTileTransform(List<Transform> availableTiles)
    {
        int randomIndex = Random.Range(0, availableTiles.Count);
        Transform selectedTransform = availableTiles[randomIndex];
        
        return selectedTransform;
    }

    private GameObject GetRandomPrefab()
    {
        if (_tileVariants == null || _tileVariants.Count == 0)
        {
            Debug.LogWarning("Tile variants list is null or empty.");
            return null;
        }

        List<Transform> tileVariants = new List<Transform>();
        foreach (TileSO tileVariant in _tileVariants)
        {
            tileVariants.Add(tileVariant.prefab);
        }

        if (tileVariants.Count == 0)
        {
            Debug.LogWarning("No tile variants available.");
            return null;
        }

        int randomIndex = Random.Range(0, tileVariants.Count);
        GameObject selectedTile = tileVariants[randomIndex].gameObject;
        return selectedTile;
    }

    private void Reset()
    {
        _tileVariants = null;
    }
    
}