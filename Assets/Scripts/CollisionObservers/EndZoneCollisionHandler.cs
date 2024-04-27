using UnityEngine.SceneManagement;

public class EndZoneCollisionHandler: ICollisionHandler<IEndZone>
{
    readonly Character _character;

    public EndZoneCollisionHandler(Character character)
    {
        _character = character;
    }
        
    public void HandleCollision(IEndZone collidedObject)
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        DataPersistanceManager.current.CompleteLevel(currentLevelName);
    }
}//  PlayerData.current.UpdateLevelProgress();