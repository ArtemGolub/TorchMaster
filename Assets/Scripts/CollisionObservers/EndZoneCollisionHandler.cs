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
        PlayerData.current.UpdateLevelProgress();
        SceneManager.LoadScene("MainMenu");
    }
}