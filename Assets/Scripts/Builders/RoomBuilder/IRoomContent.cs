using System.Collections.Generic;
using UnityEngine;

public interface IRoomContent
{
    int ContentCapacity { get; set; }
    List<ItemSO> PossbileItems { get; set; }
    List<CharacterSO> PossibleEnemies { get; set; }
    bool isStartRoom { get; set; }
}
