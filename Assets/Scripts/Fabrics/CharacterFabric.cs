using System.Collections.Generic;
using UnityEngine;

public class CharacterFabric: MonoBehaviour
{
    public static CharacterFabric current;
    
    [SerializeField]private Transform startPoint;
    private List<Transform> spawnPoints;

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
    
    public Transform SpawnCharacter(CharacterSO preset)
    {
        switch (preset.characterType)
        {
            case CharacterType.Player:
            {
                var obj = Instantiate(preset.prefab, startPoint.position, startPoint.rotation);
                return obj;
            }
        }

        return null;
    }
    
}