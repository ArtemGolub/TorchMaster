using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterFabric: MonoBehaviour
{
    public static CharacterFabric current;
    
    [SerializeField]private Transform startPoint;
    [SerializeField]private List<Transform> spawnPoints;

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

    public Transform TileSpawnCharacter(Tile tile, List<Transform> spawnPoints, List<Transform> patrolPoints)
    {
        CharacterSO characterSO = RandomiseEnemy(tile.PossibleEnemies);
        Transform randomSpawnPoint = TileRandomiseSpawnPoint(spawnPoints);

        Transform character = Instantiate(characterSO.prefab, randomSpawnPoint);
        
        // TODO Refactor
        character.GetComponent<Enemy>().Character.patrolPoints = patrolPoints;
        
        return character;
    }
    public Transform SpawnCharacter(CharacterSO preset)
    {
        switch (preset.characterType)
        {
            case CharacterType.Player:
            {
                var obj = Instantiate(preset.prefab, startPoint.position, startPoint.rotation);
                EnemyMovementController.current.player = obj.GetComponent<Player>();
                return obj;
            }
            case CharacterType.Enemy:
            {
                Transform randomSpawnPoint = RandomiseSpawnPoint();
                var obj = Instantiate(preset.prefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
                return obj;
            }
        }
        return null;
    }

    public Transform SpawnCharacterAtPoint(CharacterSO preset, Transform spawnPoint)
    {
        var character = Instantiate(preset.prefab, spawnPoint.position, spawnPoint.rotation);
        character.SetParent(spawnPoint);
        if ( character.GetComponent<Enemy>().Character.patrolPoints == null)
        {
            character.GetComponent<Enemy>().Character.patrolPoints = spawnPoint.GetComponentInParent<Room>().PatrolPoints;
        }
        return character;
    }

    private CharacterSO RandomiseEnemy(List<CharacterSO> possibleEnimies)
    {
        var randomIndex = Random.Range(0, possibleEnimies.Count);
        return possibleEnimies[randomIndex];
    }
    private Transform TileRandomiseSpawnPoint(List<Transform> spawnPoints)
    {
        var randomIndex = Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex];
    }
    private Transform RandomiseSpawnPoint()
    {
        var randomIndex = Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex];
    }
}