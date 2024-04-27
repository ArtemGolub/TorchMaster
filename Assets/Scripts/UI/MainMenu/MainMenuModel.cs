using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuModel
{
    public void DisableCanvas(Canvas canvas)
    {
        canvas.enabled = false;
    }
    public void EnableCanvas(Canvas canvas)
    {
        canvas.enabled = true;
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
