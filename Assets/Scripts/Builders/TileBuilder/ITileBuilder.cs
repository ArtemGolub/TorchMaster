using System.Collections.Generic;

public interface ITileBuilder
{
    Tile GetTile();
    void SetName(string name);
    void SetID(int id);
    
    void SetPossibleEnemies(List<CharacterSO> possibleEnimies);
    void SetPossibleItems(List<ItemSO> possibleItems);
}
