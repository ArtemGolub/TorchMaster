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

    public void LoadLevel(string levelName, LoadingScreenController loadingScreenController)
    {
        if (loadingScreenController == null)
        {
            Debug.LogError("Lost Loading Screen"); 
            return;
        }
        loadingScreenController.LoadNewScene(levelName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
