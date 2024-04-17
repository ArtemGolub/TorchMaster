public sealed class TileDirector
{
    private ITileBuilder _tileBuilder;
    
    public Tile CreateTile(RoomSO roomData)
    {
        _tileBuilder = new TileBuilder();
        
        SetParams(roomData);
        
        Tile tile =  _tileBuilder.GetTile();
        return tile;
    }

    private void SetParams(RoomSO roomData)
    {
        _tileBuilder.SetPossibleItems(roomData.possibleItems);
        _tileBuilder.SetPossibleEnemies(roomData.possibleEnimies);
    }
}
