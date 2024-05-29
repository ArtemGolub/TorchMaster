using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndZone: MonoBehaviour, IEndZone
{
    [SerializeField]private Canvas winCanvas;
    [SerializeField]private  Button menu;
    [SerializeField]private  Button nextLevel;
    [SerializeField] private SettingsCanvas settingCanvas;
    private void Start()
    {
        winCanvas.enabled = false;
    }

    public void OnWin()
    {
        winCanvas.enabled = true;
        settingCanvas.EnableCanvas(false);
        
        menu.onClick.AddListener(LoadMainMenu);
        
        string NextLevelName = DataPersistanceManager.current.GetNextLevel(SceneManager.GetActiveScene().name);
        if (NextLevelName == null)
        {
            nextLevel.gameObject.SetActive(false);
        }
        else
        {
            nextLevel.onClick.AddListener(ToNextLevel);
        }
    }
    
    
    private void ToNextLevel()
    {
        string NextLevelName = DataPersistanceManager.current.GetNextLevel(SceneManager.GetActiveScene().name);
        if (NextLevelName == null)
        {
            nextLevel.gameObject.SetActive(false);
            return;
        }
        LoadingScreenController.current.LoadNewScene(NextLevelName);
    }

    private void LoadMainMenu()
    {
        var LoadingScreenController = FindObjectOfType<LoadingScreenController>();
        LoadingScreenController.LoadNewScene("MainMenu");
    }

}