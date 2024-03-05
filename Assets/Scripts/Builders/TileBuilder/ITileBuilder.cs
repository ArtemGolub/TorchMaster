using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileBuilder
{
    Tile GetTile();
    void SetName(string name);
    void SetID(int id);

    void SetNextTileVariants(List<TileSO> nextTileVariants);
    void SetItemSpawnPoints(List<Transform> itemSpawnPoints);

    void SetPossibleEnemies(List<CharacterSO> possibleEnimies);
    void SetCharacterSpawnPoints(List<Transform> characterSpawnPoints);
    
   // void SetNextTileSpawnPoints(Dictionary<Transform, bool> nextTileSpawnPoints);
    
    void SetPossibleItems(List<ItemSO> possibleItems);
}
