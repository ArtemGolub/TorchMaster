using System.Collections.Generic;
using UnityEngine;

public sealed class TileDirector
{
    private ITileBuilder _tileBuilder;
    
    public Tile CreateTile(TileSO tileData)
    {
        _tileBuilder = new TileBuilder();
        
        SetParams(tileData);
        
        Tile tile =  _tileBuilder.GetTile();
        return tile;
    }

    private void SetParams(TileSO tileData)
    {
        _tileBuilder.SetName(tileData.Name);
        _tileBuilder.SetID(tileData.ID);
        _tileBuilder.SetNextTileVariants(tileData.NextTileVariants);
        _tileBuilder.SetPossibleItems(tileData.possibleItems);
    }
}
