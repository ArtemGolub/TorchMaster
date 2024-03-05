using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBuilder : ITileBuilder
{
    private Tile _tile = new Tile();
    
    public Tile GetTile()
    {
        return _tile;
    }

    public void SetName(string name)
    {
        _tile.Name = name;
    }

    public void SetID(int id)
    {
        _tile.ID = id;
    }

    public void SetNextTileVariants(List<TileSO> nextTileVariants)
    {
        _tile.NextTileVariants = nextTileVariants;
    }

    public void SetItemSpawnPoints(List<Transform> itemSpawnPoints)
    {
        _tile.ItemSpawnPoints = itemSpawnPoints;
    }

    public void SetPossibleEnemies(List<CharacterSO> possibleEnimies)
    {
        _tile.PossibleEnemies = possibleEnimies;
    }

    public void SetCharacterSpawnPoints(List<Transform> characterSpawnPoints)
    {
        _tile.CharacterSpawnPoints = characterSpawnPoints;
    }

    public void SetNextTileSpawnPoints(Dictionary<Transform, bool> nextTileSpawnPoints)
    {
        _tile.NextTileSpawnPoints = nextTileSpawnPoints;
    }

    public void SetPossibleItems(List<ItemSO> possibleItems)
    {
        _tile.PossibleItems = possibleItems;
    }
}
